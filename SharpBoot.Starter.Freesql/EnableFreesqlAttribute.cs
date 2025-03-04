using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Freesql
{
    [ComponentScan("SharpBoot.Starter.Freesql")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableFreesqlAttribute : Attribute
    {
    }
}
