using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Models
{
    public class Gateway
    {
        /// <summary>
        /// 服务的前缀
        /// </summary>
        public string UpUrlPrefix { get; set; }
        public string[] UpMethods { get; set; }
        public string ServiceName { get; set; }
        /// <summary>
        /// 负载均衡策略  Random RoundRobin
        /// </summary>
        public string LoadBalancing { get; set; }
    }
}
