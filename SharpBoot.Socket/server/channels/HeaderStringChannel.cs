using SharpBoot.Sockets.common;
using SharpBoot.Sockets.common.buffer_coders;
using SharpBoot.Sockets.common.channel_msgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace SharpBoot.Sockets.server.channels
{
    public class HeaderStringChannel : IDisposable
    {
        private DefaultChannel<HeaderStringChannelMsg> innelChannel;

        public HeaderStringChannel()
        {

        }
        public void Init(SharpChannel channel)
        {
            innelChannel = new DefaultChannel<HeaderStringChannelMsg>(channel, new HeaderStringBufferCoder());
            innelChannel.NewMessage += InnelChannel_NewMessage;
            innelChannel.Disposed += Dispose;
        }



        private void InnelChannel_NewMessage(HeaderStringChannelMsg msg)
        {
            ConsoleLogger.Info($"[Server接收] bodyLength={msg.BodyLength}  realLength={msg.BodyBuffer?.Length}");
        }


        public void Dispose()
        {
            innelChannel?.Dispose();
        }



    }
}
