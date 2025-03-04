using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Model;
using SharpBoot.Interceptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Startups
{
    [Order(int.MinValue)]
    [Component]
    public class RunningStartup : IStartupConfig
    {
        [Value("SharpBoot:StaticFile")] StaticFileConfig staticfileConfig;
        [Value("SharpBoot:AllowAllOrigins")] bool allowAllOrigins;
        [Value("SharpBoot:AllowAllOptionsOrigins")] bool allowAllOptionsOrigins;

        public void ConfigureServices(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            if (allowAllOrigins)
            {
                app.UseMiddleware<AllowAllOriginsMiddleware>(allowAllOptionsOrigins);
            }

            if (staticfileConfig != null && !string.IsNullOrEmpty(staticfileConfig.LocalPath) && staticfileConfig.Enable)
            {
                app.UseDefaultFiles();
                string staticFilePath = staticfileConfig.LocalPath;
                if (!Directory.Exists(staticFilePath)) Directory.CreateDirectory(staticFilePath);
                string urlPath = staticfileConfig.LocalPath;
                if (!string.IsNullOrEmpty(staticfileConfig.UrlPath)) urlPath = staticfileConfig.UrlPath;
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), staticFilePath)),
                    RequestPath = new PathString(urlPath)
                });
            }
        }
    }
}
