using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.Log4netDashBoard.Demo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Log4net.DashBoard.Config
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        [Value("Log4netDashBoard:Enable")] bool enableConfig;
        [Value("Log4netDashBoard:Url")] string logUrl;
        public void Configure(IApplicationBuilder app)
        {
            if (!enableConfig) return;
            string url = "/logui";
            if (!string.IsNullOrEmpty(logUrl)) url = logUrl;
            app.Map(url, apt =>
            {
                apt.Run(async context =>
                {
                    context.Response.Redirect("/staticfiles/html/logdashboard/index.html");
                });
            });
            app.Map(url + "-api", apt =>
            {
                apt.Run(async context =>
                {
                    context.Request.Query.TryGetValue("keyword", out var keyword);
                    context.Request.Query.TryGetValue("fetchcount", out var fetchcount);
                    int.TryParse(fetchcount, out int fv);
                    var list = LogCacher.Instance.Query(keyword, fv);
                    var result = new
                    {
                        code = 0,
                        msg = "",
                        data = list
                    };
                    var json = JsonConvert.SerializeObject(result);
                    var buffer = Encoding.UTF8.GetBytes(json);
                    await context.Response.BodyWriter.WriteAsync(buffer);
                });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (!enableConfig) return;
        }
    }
}
