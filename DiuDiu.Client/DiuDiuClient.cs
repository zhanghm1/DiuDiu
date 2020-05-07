using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace DiuDiu
{
    public class DiuDiuClient : IDiuDiuClient
    {
        public readonly DiuDiuOption  option;
        private readonly IHttpClientFactory _httpClientFactory;
        public DiuDiuClient(IHttpClientFactory httpClientFactory, DiuDiuOption _option)
        {
            _httpClientFactory = httpClientFactory;
            option = _option;
        }
        public Task<IEnumerable<DiuDiuService>>  GetService()
        {

            return null;
        }

        public async Task<bool> RegisterService(DiuDiuService service)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string conStr = JsonConvert.SerializeObject(service);
            StringContent content = new StringContent(conStr);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var resp = await httpClient.PostAsync(option.Address + "/service", content);
            if (resp.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
