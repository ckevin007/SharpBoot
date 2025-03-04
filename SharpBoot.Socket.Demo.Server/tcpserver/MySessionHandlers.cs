using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component(ComponentLifeTime.Transient)]
    public class MySessionHandlers : SessionHandlers
    {

    }
}
