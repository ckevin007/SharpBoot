using log4net;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharpBoot.Common.Attributes;
using SharpBoot.Sockets.Demo.Common.filter;
using SharpBoot.Sockets.Demo.Common.model;
using SharpBoot.Sockets.Demo.Server.service.runner;
using SharpBoot.Starter.Log4net;
using SharpBoot.Starter.SuperSockets.servers;
using SharpBoot.Starter.SuperSockets.servers.services;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component]
    public class MyAppServer
    {

        [Autowired] IServiceProvider serviceProvider;

        private readonly ILog log = LogFactory.GetLogger<MyAppServer>();

        public Task StartAsync()
        {
            SharpTcpServer<MyAppSession, MyPackageInfo, MyPackagePipelineFilter> server =
                new SharpTcpServer<MyAppSession, MyPackageInfo, MyPackagePipelineFilter>(IPAddress.Any, 5600);
            server.NewSessionConnected += Server_NewSessionConnected;
            return server.StartAsync();
        }

        private void Server_NewSessionConnected(MyAppSession session)
        {
            var client = serviceProvider.GetService<MyAppClient>();
            client.OnNewSession(session);
        }
    }
}
