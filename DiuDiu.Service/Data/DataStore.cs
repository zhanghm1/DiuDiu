using DiuDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Data
{
    public static class DataStore
    {
        /// <summary>
        /// API服务节点
        /// </summary>
        public static List<Service> Services = new List<Service>();

        /// <summary>
        /// 网关集合
        /// </summary>
        public static List<Gateway> Gateways = new List<Gateway>();

        /// <summary>
        /// 上游URL的规则
        /// </summary>
        public static List<Gateway> GatewayUpUrls = new List<Gateway>();

        public static int LoadBalancingRoundRobin = 1;
    }
}