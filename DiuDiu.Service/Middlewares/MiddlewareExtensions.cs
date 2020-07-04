using DiuDiu.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiuDiu
{
    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseDiuDiu(this IApplicationBuilder builder)
        {
            // AddRouterMap(builder);

            return builder.UseMiddleware<DiuDiuGatewayMiddleware>();
        }
    }
}