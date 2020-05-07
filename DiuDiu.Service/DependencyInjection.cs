using AutoMapper;
using DiuDiu.Data;
using DiuDiu.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiuDiu
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDiuDiu(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddControllers();
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddHttpClient();

            var Gateways = Configuration.GetSection("Gateway");

            DataStore.Gateways = Gateways.Get<List<Gateway>>();

            return services;
        }
    }
}
