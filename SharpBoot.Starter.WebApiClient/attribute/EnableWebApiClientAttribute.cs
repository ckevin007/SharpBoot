using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.WebApiClient.attribute
{
    [ComponentScan("SharpBoot.Starter.WebApiClient")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableWebApiClientAttribute : Attribute
    {

    }
}
