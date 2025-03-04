using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.service.test
{
    [Component]
    public class RabbitMQTest : IApplicationRunner
    {
        //[Autowired] TestMQProducter sender;
        public void Run(string[] args = null)
        {
            // sender.Publish("woca");
        }
    }
}
