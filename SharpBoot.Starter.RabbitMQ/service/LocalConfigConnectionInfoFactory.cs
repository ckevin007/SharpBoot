using RabbitMQ.Helper.Models;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.RabbitMQ.service
{
    [Component]
    public class LocalConfigConnectionInfoFactory : IConnectionInfoFactory
    {
        [Value("RabbitMQ")] ConnectionInfo rabbitConnection;

        public ConnectionInfo GetConnectionInfo()
        {
            return rabbitConnection;
        }
    }
}
