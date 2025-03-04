using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Sockets.Demo.Common.filter;
using SharpBoot.Sockets.Demo.Common.model;
using SharpBoot.Sockets.Demo.Server.tcpserver;
using SharpBoot.Sockets.server;
using SharpBoot.Starter.Log4net;
using SharpBoot.Starter.SuperSockets.servers;
using SuperSocket.Channel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.Demo.Server.service.runner
{
    [Component]
    public class TcpServerRunner : IApplicationRunner
    {
        private readonly ILog log = LogFactory.GetLogger<TcpServerRunner>();

        [Autowired] MyAppServer appServer;
        public void Run(string[] args = null)
        {
            appServer.StartAsync();
        }


    }
}
