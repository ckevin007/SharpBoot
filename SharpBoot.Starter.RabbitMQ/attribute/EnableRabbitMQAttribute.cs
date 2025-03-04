using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.RabbitMQ.attribute
{
    [ComponentScan("SharpBoot.Starter.RabbitMQ")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableRabbitMQAttribute : Attribute
    {
    }
}
