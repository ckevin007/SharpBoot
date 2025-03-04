using RabbitMQ.Helper.Injection.Producters;
using RabbitMQ.Helper.Models;
using RabbitMQ.Helper.Providers;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.service
{
    //  [Component]
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
