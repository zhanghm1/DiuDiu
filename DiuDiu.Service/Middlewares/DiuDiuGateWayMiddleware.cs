using DiuDiu.Data;
using DiuDiu.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiuDiu
{
    public class DiuDiuGatewayMiddleware
    {
        private readonly RequestDelegate _next;
        private IHttpClientFactory _httpClientFactory;
        public DiuDiuGatewayMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            var list = DataStore.Gateways;
            if (list == null)
            {
                await _next.Invoke(context);
            }
            else 
            {
                string Path = context.Request.Path.Value.ToLower();
                string[] urls = Path.Split("/");

                var selectList = list.Where(a => a.UpUrlPrefix== urls[1]).ToList();

                if (selectList.Count > 0)
                {
                    Gateway gateway = selectList.FirstOrDefault();
                    var services = DataStore.Services.Where(a => a.Error == 0 && a.Name == gateway.ServiceName).OrderBy(a=>a.CreateTime).ToList();
                    if(services.Count==0)
                    {
                        throw new Exception("没有对应的服务");
                    }
                    var service = services.FirstOrDefault();
                    if (gateway.LoadBalancing == LoadBalancing.Random.ToString())
                    {
                        //随机策略
                        if (services.Count > 1)
                        {
                            var rng = new Random().Next(0, services.Count - 1);
                            service = services[rng];
                        }
                    } 
                    else if (gateway.LoadBalancing == LoadBalancing.RoundRobin.ToString())
                    {
                        //循环策略
                        var  _service = services.Where(a => a.RequestNumber < DataStore.LoadBalancingRoundRobin).Take(1).FirstOrDefault();
                        if (_service == null)
                        {
                            services.ForEach(a => {
                                a.RequestNumber = DataStore.LoadBalancingRoundRobin;
                            });
                            DataStore.LoadBalancingRoundRobin++;
                            _service = services.Where(a => a.RequestNumber < DataStore.LoadBalancingRoundRobin).Take(1).FirstOrDefault();
                        }

                        if (_service!=null) 
                        {
                            service = _service;
                        }
                    }
                    
                    string downPath = $"http://{service.Host}:{service.Port}" +
                        $"/{context.Request.Path.Value.Substring(gateway.UpUrlPrefix.Length + 2)}";

                    //构造请求
                    var httpClient = _httpClientFactory.CreateClient();

                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = new HttpMethod(context.Request.Method),
                        Content = new StreamContent(context.Request.Body),
                        RequestUri = new Uri(downPath),
                        
                    };

                    foreach (var header in context.Request.Headers)
                    {
                        httpRequestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                    
                    // 发送请求
                    var resp = await httpClient.SendAsync(httpRequestMessage);

                    //处理返回
                    CloneResponseHeadersIntoContext(context, resp);

                    context.Response.ContentType = resp.Content.Headers.ContentType?.ToString();
                    context.Response.ContentLength = resp.Content.Headers.ContentLength;

                    await resp.Content.CopyToAsync(context.Response.Body);
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            
        }



        private void CloneResponseHeadersIntoContext(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            foreach (var header in responseMessage.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            //这个要有，不然没有返回
            context.Response.Headers.Remove("Transfer-Encoding");
        }
    }
}