using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SharpBoot.Interceptors
{
    public class AllowAllOriginsMiddleware
    {
        private static int expireSecond = 30 * 24 * 60 * 60;
        private static string max_age = $"max-age={expireSecond}";
        private readonly RequestDelegate _next;
        private bool _allowAllOrigins;


        public AllowAllOriginsMiddleware(RequestDelegate next, bool allowOptions)
        {
            _next = next;
            _allowAllOrigins = allowOptions;
        }
        public async Task Invoke(HttpContext context)
        {
            // context.Response.Headers.Add("Cache-Control", "no-store");
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
            if (_allowAllOrigins)
            {
                if (context.Request.Method.ToLower() == "options")
                {
                    context.Response.StatusCode = 204;
                    return;
                }
            }
            await _next(context);
        }
    }
}
