using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Starter.Log4net;

namespace SharpBoot.Demo.Middlewares
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILog log = LogFactory.GetLogger<TestMiddleware>();
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            log.Info("执行到中间件了");
            return _next.Invoke(context);
        }
    }
}
