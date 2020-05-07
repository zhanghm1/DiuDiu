using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
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
            builder.UseMiddleware<DiuDiuControllerMiddleware>();
            
            return builder.UseMiddleware<DiuDiuGatewayMiddleware>();
        }
    }
}
