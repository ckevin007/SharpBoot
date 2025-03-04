using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharpBoot.Starter.SuperSockets.servers.sessions;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.SuperSockets.servers.services
{
    public class SharpSocketService<TPackage> : SuperSocketService<TPackage>
    {
        private readonly IServiceProvider serviceProvider;

        public event Action<SharpAppSession<TPackage>> NewSessionConnected;
        public event Action<SharpAppSession<TPackage>, CloseEventArgs> SessionClosed;

        public SharpSocketService(IServiceProvider serviceProvider, IOptions<ServerOptions> serverOptions) : base(serviceProvider, serverOptions)
        {
            this.serviceProvider = serviceProvider;

        }


        public void OnSessionClosed(IAppSession session, CloseEventArgs e)
        {
            if (session is SharpAppSession<TPackage> sa)
            {
                SessionClosed?.Invoke(sa, e);
            }
        }

        protected override ValueTask OnSessionConnectedAsync(IAppSession session)
        {
            if (session is SharpAppSession<TPackage> sa)
            {
                NewSessionConnected?.Invoke(sa);
            }
            return base.OnSessionConnectedAsync(session);
        }
    }
}
