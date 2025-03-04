using SuperSocket;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.SuperSockets.servers.sessions
{
    public class SharpAppSessionFactory<TAppSession> : ISessionFactory where TAppSession : AppSession
    {
        public Type SessionType => throw new NotImplementedException();



        public IAppSession Create()
        {
            throw new NotImplementedException();
        }
    }
}
