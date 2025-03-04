using Microsoft.AspNetCore.Hosting;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Extenssions;
using SharpBoot.Common.Service;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpBoot.Common.Extenssion
{
    public static class BuilderTypeExtensions
    {

        public static void UseSharpBoot(this IWebHostBuilder builder, List<Assembly> assemblyList)
        {
            var builderTypes = AttributeExtension.GetAttributeMarkTypes<WebHostBuilderConfigurationAttribute>(assemblyList, true);
            if (builderTypes != null)
            {
                foreach (var type in builderTypes)
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is IWebHostBuilderConfigurationer configurationer)
                    {
                        configurationer.BeforeBuild(builder);
                    }
                }
            }
        }
    }
}
