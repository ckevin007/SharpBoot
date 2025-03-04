using SharpBoot.Common.Attributes;
using SharpBoot.Sockets.Demo.Common.model;
using SharpBoot.Sockets.Demo.Server.service.runner;
using SharpBoot.Starter.SuperSockets.servers.sessions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    public class MyAppSession : SharpAppSession<MyPackageInfo>
    {

        protected override ValueTask OnSessionConnectedAsync()
        {
            return base.OnSessionConnectedAsync();
        }



    }
}
