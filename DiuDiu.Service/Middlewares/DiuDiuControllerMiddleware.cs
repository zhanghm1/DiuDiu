using DiuDiu.Data;
using DiuDiu.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiuDiu
{
    public class DiuDiuControllerMiddleware
    {
        private readonly RequestDelegate _next;
        public DiuDiuControllerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}