using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore.V2;
using Nacos.V2;
using Nacos.V2.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace SharpBoot.Starter.Nacos.Config
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        [Autowired] IConfiguration config;

        [Value("Nacos:EnableConfig")] bool enableConfig;
        [Value("Nacos:EnableDiscovery")] bool enableDiscovery;


        public void ConfigureServices(IServiceCollection services)
        {
            if (enableConfig)
            {
                services.AddNacosV2Config(config, null, "Nacos");
            }
            if (enableDiscovery)
            {
                //  services.AddNacosV2Naming(config, null, "Nacos");

                services.AddNacosAspNet(config, "Nacos");
            }
        }


        public void Configure(IApplicationBuilder app)
        {
            var coinfig = app.ApplicationServices.GetService<INacosConfigService>();
        }


    }
}
