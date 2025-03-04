using SharpBoot.Sockets.common;
using SharpBoot.Sockets.common.buffer_coders;
using SharpBoot.Sockets.common.buffer_utils;
using SharpBoot.Sockets.common.channel_msgs;
using SharpBoot.Sockets.server;
using SharpBoot.Sockets.server.channels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.client.channels
{
    public class HeaderStringClient : IDisposable
    {
        private DefaultClient<HeaderStringChannelMsg> innelChannel;

        public void Init(string ip, int port, bool autoReconnect = true)
        {
            innelChannel = new DefaultClient<HeaderStringChannelMsg>(ip, port, new HeaderStringBufferCoder(), autoReconnect);
            innelChannel.NewMsg += InnelChannel_NewMsg;
        }

        public Task<bool> ConnectAsync()
        {
            return innelChannel.ConnectAsync();
        }

        private void InnelChannel_NewMsg(HeaderStringChannelMsg msg)
        {
            ConsoleLogger.Info($"[Client接收] bodyLength={msg.BodyLength}  realLength={msg.BodyBuffer?.Length}");
        }

        public Task<bool> SendAsync(HeaderStringChannelMsg msg)
        {
            return innelChannel.SendAsync(msg);
        }

        public void Dispose()
        {
            innelChannel?.Dispose();
        }
    }
}
