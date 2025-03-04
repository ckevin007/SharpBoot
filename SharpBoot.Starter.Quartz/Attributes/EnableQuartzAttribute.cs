using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Quartz.Attributes
{
    [ComponentScan("SharpBoot.Starter.Quartz")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableQuartzAttribute : Attribute
    {
    }
}
