using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SharpBoot.Swagger.Starter
{
    [Component]
    public class Starter : IStartupConfig
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Server API",
                    Description = "简要说明",
                    TermsOfService = new Uri("http://localhost:8080/"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "开发人员",
                        Email = string.Empty,
                        Url = new Uri("http://localhost:8080/")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "@License",
                        Url = new Uri("http://localhost:8080/")
                    }
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "XML", "swagger.xml");
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //  c.RoutePrefix = string.Empty;  //设置为空，可将 swagger 设置为主页 http://localhost:<port>/swagger
            });
            app.UseDefaultFiles();
        }
    }
}
