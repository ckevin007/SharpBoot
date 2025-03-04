
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Extenssions;
using SharpBoot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Autofac.Extensions.DependencyInjection;
using SharpBoot.Common.Service;
using SharpBoot.Common.Extenssion;
using SharpBoot.Common.Utils;

namespace SharpBoot
{
    public static class SharpBootApplication
    {
        public static string[] StartArgs { get; set; }
        public static Type StartType { get; set; }
        public static List<Assembly> AssemblyList { get; set; }



        private static void Init()
        {
            AssemblyList = GetProjectAssemblyList();
        }

        public static List<Assembly> GetProjectAssemblyList(Type startType = null)
        {
            return AssemblyUtil.GetSharpBootApplicationAssemblys(startType ?? StartType, typeof(SharpBootApplication).Assembly);
        }

        public static void Run<T>(string[] args = null, List<string> jsonSettingFileList = null)
        {
            Run(typeof(T), args, jsonSettingFileList);
        }
        public static void Run(Type startType, string[] args = null, List<string> jsonSettingFileList = null)
        {
            StartArgs = args;
            StartType = startType;
            Init();
            CreateHostBuilder(args, jsonSettingFileList).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args, List<string> jsonSettingFileList = null)
        {
            var tmp = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory);
            if (jsonSettingFileList == null) jsonSettingFileList = new List<string>();
            if (File.Exists("appsettings.json")) jsonSettingFileList.Add("appsettings.json");
            if (File.Exists("appsettings.Development.json")) jsonSettingFileList.Add("appsettings.Development.json");
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory);
            var appsettingDirPath = Path.Combine(directory.FullName, "appsettings");
            if (Directory.Exists(appsettingDirPath))
            {
                directory = new DirectoryInfo(appsettingDirPath);
                foreach (var file in directory.GetFiles())
                {
                    if (file.Extension.ToLower() == ".json")
                    {
                        jsonSettingFileList.Add(file.Name);
                    }
                }
            }

            foreach (var itm in jsonSettingFileList)
            {
                tmp.AddJsonFile(itm);
            }
            var configuration = tmp.Build();

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            builder.UseConfiguration(configuration);
            builder.UseSharpBoot(AssemblyList);
            builder.UseStartup<Startup>();
            return builder;
        }

    }
}
