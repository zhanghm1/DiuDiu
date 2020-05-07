using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiuDiu
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDiuDiu(this IServiceCollection services,Action<DiuDiuOption> action)
        {
            DiuDiuOption option = new DiuDiuOption();
            action(option);
            services.AddSingleton(option);
            services.AddHttpClient();
            services.AddSingleton<IDiuDiuClient, DiuDiuClient>();
            return services;
        }
    }
}
