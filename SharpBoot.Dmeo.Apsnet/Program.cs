using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharpBoot.Common.Extenssion;
using SharpBoot.Starter.Caching;
using SharpBoot.Starter.Nacos.Attributes;
using SharpBoot.Starter.Redis;
using SharpBoot.Starter.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Dmeo.Apsnet
{
    [EnableSwagger]
    [EnableNacos]
    [EnableRedis]
    [EnableCaching]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSharpBoot(SharpBootApplication.GetProjectAssemblyList(typeof(Program)));
                });
    }
}
