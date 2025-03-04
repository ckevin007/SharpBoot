using IdGen.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Startups
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        public void Configure(IApplicationBuilder app)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdGen(0);
        }
    }
}
