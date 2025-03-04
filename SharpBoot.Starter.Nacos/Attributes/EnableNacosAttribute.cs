using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Nacos.Attributes
{
    [ComponentScan("SharpBoot.Starter.Nacos")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableNacosAttribute : Attribute
    {
    }
}
