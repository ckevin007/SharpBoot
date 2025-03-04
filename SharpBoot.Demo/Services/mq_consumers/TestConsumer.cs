using log4net;
using RabbitMQ.Helper.Injection.Attributes;
using RabbitMQ.Helper.Injection.Interfaces;
using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.mq_consumers
{
    [Component]
    public class TestConsumer : IConsumer
    {
        private readonly ILog log = LogFactory.GetLogger<TestConsumer>();

        /// <summary>
        /// 也可写全队列参数
        /// </summary>
        /// <param name="msg"></param>
        [RabbitSubscribe(
            Exchange = "sharpboot.test.exchange",
            Routing = "",
            Queue = "sharpboot.test.queue",
            Durable = true,
            AutoDelete = false,
            Exclusive = false,
            PerfetchCount = 1,
            AutoAck = false)] //PerfetchCount>1时候，若AutoAck=true,则不生效
        public bool Receive(string msg)
        {
            log.Info($"[接收 id=1] msg=" + msg);
            Task.Delay(2000).Wait();
            return true;
        }



        /// <summary>
        /// 也可写全队列参数
        /// </summary>
        /// <param name="msg"></param>
        //[RabbitSubscribe(
        //    Exchange = "sharpboot.test.exchange",
        //    Routing = "",
        //    Queue = "sharpboot.test.queue",
        //    Durable = true,
        //    AutoDelete = false,
        //    Exclusive = false)]
        public void Receive2(string msg)
        {
            log.Info($"[接收 id=2] msg=" + msg);
        }
    }
}
