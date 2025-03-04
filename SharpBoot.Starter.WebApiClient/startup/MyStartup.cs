using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Extenssions;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.WebApiClient.attribute;
using SharpBoot.Starter.WebApiClient.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApiClient;
using SharpBoot;

namespace SharpBoot.Starter.WebApiClient.startup
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        [Autowired] IConfiguration configuration;

        public void Configure(IApplicationBuilder app)
        {

        }
        public void ConfigureServices(IServiceCollection services)
        {
            Type[] types = SharpBootApplication.AssemblyList.SelectMany(a =>
            {
                return AttributeExtension.GetAttributeMarkTypes<WebApiAttribute>(a, true);
            }).ToArray();

            types?.ToList().ForEach(t =>
            {
                services.AddHttpApi(t).ConfigureHttpClient(client =>
                {
                    WebApiAttribute attribute = t.GetCustomAttribute<WebApiAttribute>();
                    WebApiOption option = attribute.Option;
                    if (option == null) option = configuration.GetSection(attribute.OptionConfigName).Get<WebApiOption>();
                    if (option == null) throw new Exception("WebApiOption配置不可为空");
                    client.BaseAddress = new Uri(option.Url);
                    if (option.TimeoutSecond > 0) client.Timeout = TimeSpan.FromSeconds(option.TimeoutSecond);
                });
            });
        }


    }
}
