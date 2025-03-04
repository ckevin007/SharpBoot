using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SharpBoot.Aspnet.Extensions
{
    public static class StartupExtension
    {
        private static Startup MyStartup;
        public static void AddSharpBoot(this IServiceCollection services, Type mainEntryPointType = null, string[] args = null)
        {
            if (mainEntryPointType == null)
            {
                var methodInfo = Assembly.GetCallingAssembly().EntryPoint;
                if (methodInfo == null) methodInfo = Assembly.GetExecutingAssembly().EntryPoint;
                if (methodInfo == null) throw new Exception("无法获取启动类");
                mainEntryPointType = methodInfo.DeclaringType;
            }
            if (mainEntryPointType == null) throw new Exception("请设定启动类");
            SharpBootAspnetApplication.Run(mainEntryPointType, args);

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            MyStartup = new Startup(configuration);
            MyStartup.ConfigureServices(services);
        }

        public static void UseSharpBoot(this IApplicationBuilder app)
        {
            MyStartup.Configure(app);
        }
    }
}
