using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;

namespace SharpBoot.Starter.Swagger
{
    [ComponentScan("SharpBoot.Starter.Swagger")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableSwaggerAttribute : Attribute
    {

    }
}
