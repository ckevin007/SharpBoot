using SharpBoot.Common.Attributes;
using SharpBoot.Sockets.Demo.Common.model;
using SuperSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component(SharpBoot.Common.Enums.ComponentLifeTime.Transient)]
    public class MyPackageHandler : IPackageHandler<MyPackageInfo>
    {
        public ValueTask Handle(IAppSession session, MyPackageInfo package)
        {
            return new ValueTask();
        }
    }
}
