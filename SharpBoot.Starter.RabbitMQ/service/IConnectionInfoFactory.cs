using RabbitMQ.Helper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.RabbitMQ.service
{
    public interface IConnectionInfoFactory
    {
        ConnectionInfo GetConnectionInfo();
    }
}
