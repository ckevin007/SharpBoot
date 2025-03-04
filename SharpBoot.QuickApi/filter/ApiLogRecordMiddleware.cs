using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharpBoot.QuickApi.model;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.filter
{
    public class ApiLogRecordMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILog log = LogFactory.GetLogger<ApiLogRecordMiddleware>();
        public ApiLogRecordMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                string str = JsonConvert.SerializeObject(Result.Fail(e.Message));
                context.Response.Headers["content-length"] = Encoding.UTF8.GetBytes(str).Length + "";
                context.Response.Headers["content-type"] = "application/json; charset=utf-8";
                await context.Response.WriteAsync(str);
                return;
            }
            finally
            {
                stopwatch.Stop();
                string url = context.Request.Path;
                string method = context.Request.Method;
                int code = context.Response.StatusCode;
                log.Info($"[{method}] url={url} code={code} use={stopwatch.ElapsedMilliseconds}");
            }


        }
    }
}
