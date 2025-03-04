using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Sockets.Demo.Common.model;
using SharpBoot.Starter.Log4net;
using SuperSocket;
using SuperSocket.Channel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component(ComponentLifeTime.Transient)]
    public class MyAppClient
    {
        public MyAppSession Session { get; set; }

        private readonly ILog log = LogFactory.GetLogger<MyAppClient>();

        public void OnNewSession(MyAppSession session)
        {
            this.Session = session;
            log.Info($"[连接] {Session.SessionID} {Session.RemoteEndPoint}");
            session.NewPackage += Session_NewPackage;
            Session.Closed += Session_Closed;

        }

        private void Session_NewPackage(MyPackageInfo msg)
        {
            log.Info($"[接收] {msg.Body.Length}");
            IAppSession session = this.Session as IAppSession;
            session.SendAsync(msg.ToBytes());
        }

        private ValueTask Session_Closed(object sender, CloseEventArgs e)
        {
            log.Info($"[断开] {Session.SessionID} {e.Reason}");
            return new ValueTask();
        }
    }
}
