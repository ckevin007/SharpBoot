using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Services.impl;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    //[Component]
    public class RabbitMQTest : IApplicationRunner
    {
        [Autowired] TestMQProducter producter;

        private readonly ILog log = LogFactory.GetLogger<RabbitMQTest>();
        public void Run(string[] args = null)
        {
            while (true)
            {
                log.Info("send to mq");
                producter.Publish(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Task.Delay(100).Wait();
            }
        }
    }
}
