using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Nacos.Microsoft.Extensions.Configuration;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Nacos.Config
{
    [WebHostBuilderConfiguration]
    public class WebHostBuilderConfigurationer : IWebHostBuilderConfigurationer
    {
        public void BeforeBuild(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, builder) =>
            {
                var c = builder.Build();
                var tmp = c.GetSection("Nacos");
                builder.AddNacosV2Configuration(tmp);
            });
        }
    }
}
