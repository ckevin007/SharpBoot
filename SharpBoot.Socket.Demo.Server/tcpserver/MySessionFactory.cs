using SharpBoot.Common.Attributes;
using SuperSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component(SharpBoot.Common.Enums.ComponentLifeTime.Transient)]
    public class MySessionFactory : ISessionFactory
    {
        public Type SessionType => typeof(MyAppSession);

        [Autowired]
        private IServiceProvider serviceProvider;

        public IAppSession Create()
        {
            return serviceProvider.GetService(typeof(MyAppSession)) as IAppSession;
        }
    }
}
