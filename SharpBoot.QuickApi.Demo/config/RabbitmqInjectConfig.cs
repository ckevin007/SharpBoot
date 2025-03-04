using RabbitMQ.Helper.Models;
using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Nacos.Attributes;
using SharpBoot.Starter.RabbitMQ.service;
using SharpBoot.Starter.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.config
{
    // [Component(Primary = true)]
    public class RabbitmqInjectConfig : IConnectionInfoFactory
    {
        [NacosValue("sharpboot:quick-api:rabbitmq")] ConnectionInfo rabbitConnection;


        public ConnectionInfo GetConnectionInfo()
        {
            return this.rabbitConnection;
        }
    }
}
