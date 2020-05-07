using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Models
{
    public class Gateway
    {
        private string _UpUrl { get; set; }
        /// <summary>
        /// 定义第一个/的字符串为服务字符串
        /// </summary>
        public string UpUrl
        {
            get
            {
                return _UpUrl;
            }
            set
            {
                _UpUrl = value.ToLower();
                if (_UpUrl.Contains("{url}")) {

                    UpUrlArr = _UpUrl.Split("/").ToList();

                    UpUrlPrefix = "/"+ UpUrlArr[1];
                }
                
            }
        }
        public string[] UpMethods { get; set; }

        public string ServiceName { get; set; }
        /// <summary>
        /// 负载均衡策略  Random RoundRobin
        /// </summary>
        public string LoadBalancing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> UpUrlArr { get; set; }
        /// <summary>
        /// 服务的字符串
        /// </summary>
        public string UpUrlPrefix { get; set; }
    }
}
