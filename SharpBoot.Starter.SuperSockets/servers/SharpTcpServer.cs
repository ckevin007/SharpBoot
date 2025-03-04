using SharpBoot.Starter.SuperSockets.servers.services;
using SharpBoot.Starter.SuperSockets.servers.sessions;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.SuperSockets.servers
{
    public class SharpTcpServer<TSession, TPackage, TPipelineFilter> where TPipelineFilter : IPipelineFilter<TPackage>, new()
        where TSession : SharpAppSession<TPackage>
    {
        private IPAddress address;
        private int port;
        private string serverName;

        public SharpSocketService<TPackage> SharpServer { get; private set; }

        public event Action<TSession> NewSessionConnected;

        public event Action<TSession, CloseEventArgs> SessionClosed;

        public SharpTcpServer(IPAddress address, int port, string serverName = "")
        {
            this.address = address;
            this.port = port;
            this.serverName = serverName;
        }

        public Task StartAsync()
        {
            var builder = SuperSocketHostBuilder.Create<TPackage, TPipelineFilter>()
                .ConfigureSuperSocket(options =>
                {
                    options.Name = serverName;
                    options.MaxPackageLength = int.MaxValue;
                    options.Listeners = new List<ListenOptions>(){
                        new ListenOptions
                        {
                            Ip =address.ToString(),
                            Port = port,
                        }
                      };
                });

            builder.UsePackageHandler(async (session, package) =>
            {
                SharpAppSession<TPackage> s = session as SharpAppSession<TPackage>;
                await s.OnPackage(package);
            });

            builder.UseSession<TSession>();
            builder.UseSessionHandler(session => new ValueTask(), (a, b) => new ValueTask());
            builder.UseHostedService<SharpSocketService<TPackage>>();


            var iserver = builder.BuildAsServer() as SharpSocketService<TPackage>;


            SharpServer = iserver;
            iserver.NewSessionConnected += Iserver_NewSessionConnnected;
            iserver.SessionClosed += Iserver_SessionClosed;
            return (iserver as IServer).StartAsync();
        }

        private void Iserver_SessionClosed(SharpAppSession<TPackage> session, CloseEventArgs e)
        {
            SessionClosed?.Invoke(session as TSession, e);
        }

        private void Iserver_NewSessionConnnected(SharpAppSession<TPackage> session)
        {
            NewSessionConnected?.Invoke(session as TSession);
        }
    }
}
