using SharpBoot.Starter.SuperSockets.servers.services;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.SuperSockets.servers.sessions
{
    public class SharpAppSession<TPackage> : AppSession
    {
        public event Action<TPackage> NewPackage;

        protected override ValueTask OnSessionClosedAsync(CloseEventArgs e)
        {
            var server = this.Server as SharpSocketService<TPackage>;
            server.OnSessionClosed(this, e);
            return base.OnSessionClosedAsync(e);
        }

        public Task OnPackage(TPackage package)
        {
            NewPackage?.Invoke(package);
            return Task.CompletedTask;
        }

    }
}
