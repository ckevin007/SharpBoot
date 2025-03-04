using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Redis
{
    [ComponentScan("SharpBoot.Starter.Redis")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableRedisAttribute : Attribute
    {
    }
}
