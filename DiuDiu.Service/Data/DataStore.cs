using DiuDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Data
{
    public static class DataStore
    {
        public static List<Service> Services = new List<Service>();


        public static List<Gateway> Gateways = new List<Gateway>();

        /// <summary>
        /// 上游URL的规则
        /// </summary>
        public static List<Gateway> GatewayUpUrls = new List<Gateway>();


        public static int LoadBalancingRoundRobin = 0;
    }
}
