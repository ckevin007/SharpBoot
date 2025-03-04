using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;

namespace SharpBoot.Swagger.Starter
{
    [ComponentScan("SharpBoot.Swagger.Starter")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableSwaggerAttribute : Attribute
    {

    }
}
