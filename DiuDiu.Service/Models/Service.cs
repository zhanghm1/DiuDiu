﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiuDiu.Models
{
    public class Service
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string ID{ get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址  IP或者其他   http
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        public ServiceCheck Check { get; set; }

        /// <summary>
        /// 错误的次数
        /// </summary>
        public int Error { get; set; }
        /// <summary>
        /// 请求的次数 负载均衡策略使用
        /// </summary>
        public int RequestNumber { get; set; }

        public bool CheckOK { get; set; }


        public Task ChckTask { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class ServiceCheck
    {
        /// <summary>
        /// 完全地址  http://127.0.0.1:8080/helth
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 超时时间 秒
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 服务注册后多久开始检查 秒
        /// </summary>
        public int StartCheckTime { get; set; }
        /// <summary>
        /// 每次检查的间隔时间 秒
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 允许错误的次数 建议大于3次
        /// </summary>
        public int ErrorTimes { get; set; }

    }
}
