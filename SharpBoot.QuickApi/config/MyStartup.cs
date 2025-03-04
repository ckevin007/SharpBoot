using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.QuickApi.filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SharpBoot.QuickApi.config
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        [Value("Config:EnableApiLog")] bool enableApiLog;
        [Value("Config:EnableQuickStartup")] bool enableQuickStartup;


        public void ConfigureServices(IServiceCollection services)
        {
            if (!enableQuickStartup) return;
            var mvc = services.AddMvc(option =>
            {
                option.Filters.Add<ExceptionFilter>();
                option.Filters.Add<ModelValidFilter>();
            });

            mvc.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()  //CamelCaseNamingStrategy
                };
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });


            InitSwagger(services);
        }
        private void InitSwagger(IServiceCollection services)
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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "token header",
                    Name = "token",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "XML", "swagger.xml");
                c.IncludeXmlComments(xmlPath);
                c.DocumentFilter<HiddenApiFilter>();
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            if (!enableQuickStartup) return;

            if (enableApiLog) app.UseMiddleware<ApiLogRecordMiddleware>();
            //app.UseMiddleware<HeadSettingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //  c.RoutePrefix = string.Empty;  //设置为空，可将 swagger 设置为主页 http://localhost:<port>/swagger
            });
            app.UseDefaultFiles();

            string staticFilePath = "staticfiles";
            if (!Directory.Exists(staticFilePath)) Directory.CreateDirectory(staticFilePath);
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), staticFilePath)),
                RequestPath = new PathString($"/{staticFilePath}")
            });
        }
    }
}
