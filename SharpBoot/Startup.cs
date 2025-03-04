using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Extenssions;
using SharpBoot.Services.Impls;
using SharpBoot.Startups;
using SharpBoot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SharpBoot
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string prefixPath = Configuration.GetSection("SharpBoot:ContextPath").Value;
            IMvcBuilder mvc = services.AddControllersWithViews(option =>
            {
                if (!string.IsNullOrEmpty(prefixPath))
                {
                    option.UseCentralRoutePrefix(new RouteAttribute(prefixPath));
                }
            });

            ControllerFeature feature = new ControllerFeature();
            mvc.PartManager.PopulateFeature(feature);
            foreach (Type type in feature.Controllers.Select(c => c.AsType()))
            {
                mvc.Services.TryAddTransient(type, type);
            }
            mvc.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, SharpControllerActivator>());
            SharpBootApplication.AssemblyList.ForEach(a => mvc.AddApplicationPart(a));

            ComponentInjectStartup.Instance.ConfigureServices(services);

            mvc.SetCompatibilityVersion(CompatibilityVersion.Latest);
            mvc.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new DefaultNamingStrategy()  //CamelCaseNamingStrategy
            });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            var startupList = GetServiceList<IStartupConfig>(serviceProvider);
            startupList?.ForEach(a =>
            {
                a.ConfigureServices(services);
            });
            containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            container = containerBuilder.Build();
            serviceProvider = new AutofacServiceProvider(container);
            return serviceProvider;
        }

        private List<T> GetServiceList<T>(IServiceProvider serviceProvider)
        {
            List<T> startupList = serviceProvider.GetServices<T>().ToList();
            startupList = AutowiredUtils.Order(startupList.Select(a => (object)a).ToList(), null)
                .Select(a => (T)a)
                .ToList();
            return startupList;
        }

        public void Configure(IApplicationBuilder app)
        {
            ComponentInjectStartup.Instance.Configure(app);
            var startupList = GetServiceList<IStartupConfig>(app.ApplicationServices);
            startupList?.ForEach(a =>
            {
                a.Configure(app);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.ApplicationServices.GetService<ApplicationStarter>().Run(SharpBootApplication.StartArgs);
        }
    }
}
