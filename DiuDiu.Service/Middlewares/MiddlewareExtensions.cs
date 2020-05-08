using DiuDiu.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiuDiu
{
    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDiuDiu(this IApplicationBuilder builder)
        {
            AddRouterMap(builder);


            return builder.UseMiddleware<DiuDiuGatewayMiddleware>();
        }

        private static void AddRouterMap(IApplicationBuilder builder)
        {
            builder.UseRouter(a => a.MapGet("Service", async (req, resp, router) =>
            {
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    Console.WriteLine("request:  Service");
                    var controller = scope.ServiceProvider.GetService<ServiceController>();

                    var data = controller.Get();

                    await resp.WriteAsync(JsonConvert.SerializeObject(data));
                }
            }));
            builder.UseRouter(a => a.MapPost("Service", async (request, resp, router) =>
            {
                string json = string.Empty;
                using (var buffer = new MemoryStream())
                {
                    await request.Body.CopyToAsync(buffer);
                    var b = buffer.ToArray();
                    json = Encoding.UTF8.GetString(b);
                }
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    //Console.WriteLine("request:  Service");
                    var controller = scope.ServiceProvider.GetService<ServiceController>();

                    var requestModel = JsonConvert.DeserializeObject<ServiceEditDto>(json);
                    var data = controller.Post(requestModel);

                    await resp.WriteAsync(JsonConvert.SerializeObject(data));
                }

            }));
        }
    }
}
