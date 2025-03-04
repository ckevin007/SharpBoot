using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.filter
{
    public class HeadSettingMiddleware
    {
        private static int expireSecond = 30 * 24 * 60 * 60;
        private static string max_age = $"max-age={expireSecond}";
        private readonly RequestDelegate _next;
        public HeadSettingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Cache-Control", "no-store");
            await _next(context);
        }
    }
}
