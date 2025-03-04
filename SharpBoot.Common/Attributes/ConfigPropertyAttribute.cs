using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Common.Attributes
{
    [Component]
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigPropertyAttribute : Attribute
    {
        public string ConfigPath { get; }

        public ConfigPropertyAttribute(string configPath)
        {
            ConfigPath = configPath;
        }
    }
}
