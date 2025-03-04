using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Nacos.Attributes
{
    [Component]
    [AttributeUsage(AttributeTargets.Class)]
    public class NacosConfigPropertyAttribute : ConfigPropertyAttribute
    {
        public NacosConfigPropertyAttribute(string configPath) : base(configPath)
        {
        }
    }
}
