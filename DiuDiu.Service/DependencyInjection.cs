using AutoMapper;
using DiuDiu.Data;
using DiuDiu.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DiuDiu
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 添加服务依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDiuDiu(this IServiceCollection services, IConfiguration Configuration)
        {
            Assembly assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(assembly);

            services.AddHttpClient();

            //获取配置
            var Gateways = Configuration.GetSection("Gateway");
            DataStore.Gateways = Gateways.Get<List<Gateway>>();

            return services;
        }
    }
}