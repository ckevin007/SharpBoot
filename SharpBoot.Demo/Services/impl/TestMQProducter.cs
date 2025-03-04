using RabbitMQ.Helper.Injection.Producters;
using RabbitMQ.Helper.Models;
using RabbitMQ.Helper.Providers;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.impl
{
    [Component]
    public class TestMQProducter : ProducterProxy
    {
        private readonly string exchange = "sharpboot.test.exchange";
        public TestMQProducter(RabbitMQProvider provider) : base(provider)
        {
        }

        public override ProducterOptions Options => new ProducterOptions()
        {
            Exchange = exchange,
            Durable = true
        };
    }
}
