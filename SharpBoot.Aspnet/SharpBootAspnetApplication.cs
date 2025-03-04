using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpBoot.Aspnet
{
    public static class SharpBootAspnetApplication
    {
        public static void Run(Type startType, string[] args = null)
        {
            SharpBootApplication.StartType = startType;
            SharpBootApplication.StartArgs = args;
            SharpBootApplication.AssemblyList = SharpBootApplication.GetProjectAssemblyList();
        }
    }
}
