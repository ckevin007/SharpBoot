using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.SuperSockets.attribute
{
    [ComponentScan("SharpBoot.Starter.SuperSockets")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableSuperSocketAttribute : Attribute
    {

    }
}
