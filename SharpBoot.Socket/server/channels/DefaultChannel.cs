using SharpBoot.Sockets.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.server.channels
{
    public class DefaultChannel<Tmsg> : IDisposable where Tmsg : IChannelMsg
    {
        private readonly SharpChannel channel;
        private readonly IBufferCoder<Tmsg> bufferCoder;
        public event Action<Tmsg> NewMessage;
        public event Action Disposed;
        private bool isDisposed;
        public DefaultChannel(SharpChannel channel, IBufferCoder<Tmsg> bufferCoder)
        {
            this.channel = channel;
            this.bufferCoder = bufferCoder;
            bufferCoder.NewMsg += BufferCoderNewMsg;
            channel.Receive += (buffer) => bufferCoder.Decode(buffer);
            channel.Closed += () => Dispose();
        }

        public virtual void BufferCoderNewMsg(Tmsg msg)
        {
            NewMessage?.Invoke(msg);
        }

        public virtual void Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;
            Disposed?.Invoke();
            bufferCoder?.Dispose();
            this.channel?.Dispose();
        }
    }
}
