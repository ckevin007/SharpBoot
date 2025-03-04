using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Caching
{
    [ComponentScan("SharpBoot.Starter.Caching")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableCachingAttribute : Attribute
    {
    }
}
