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
        public static IServiceCollection AddDiuDiu(this IServiceCollection services, IConfiguration Configuration)
        {

            Assembly assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(assembly);

            services.AddHttpClient();


            //mvcBuilder.AddControllersAsServices();

            //mvcBuilder.AddApplicationPart(assembly);

            //mvcBuilder.ConfigureApplicationPartManager(m => {
            //    var feature = new ControllerFeature();
            //    m.ApplicationParts.Add(new AssemblyPart(assembly));
            //    m.PopulateFeature(feature);
            //    services.AddSingleton(feature.Controllers.Select(t => t.AsType()).ToArray());
            //});

            services.AddScoped<ServiceController>();

            var Gateways = Configuration.GetSection("Gateway");

            DataStore.Gateways = Gateways.Get<List<Gateway>>();

            return services;
        }
    }
}
