using SharpBoot.Sockets.common;
using SharpBoot.Sockets.common.buffer_utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.client.channels
{
    public class DefaultClient<TMsg> : IDisposable where TMsg : IChannelMsg
    {
        private SharpClient client;
        private IBufferCoder<TMsg> coder;
        public event Action<TMsg> NewMsg;


        public DefaultClient(string ip, int port, IBufferCoder<TMsg> coder, bool autoReconnect = true)
        {
            this.coder = coder;
            coder.NewMsg += Coder_NewMsg;
            client = new SharpClient(ip, port, autoReconnect);
            client.OnReceiveBytes += Client_OnReceiveBytes;
            client.OnClosed += Client_OnClosed;
        }

        private void Client_OnClosed()
        {

        }

        private void Coder_NewMsg(TMsg obj)
        {
            NewMsg.Invoke(obj);
        }

        private void Client_OnReceiveBytes(byte[] buffer)
        {
            coder.Decode(buffer);
        }

        public Task<bool> ConnectAsync()
        {
            return client.ConnectAsync();
        }

        public async Task<bool> SendAsync(TMsg msg)
        {
            byte[] buffer = ThuBufferUtils.GetBytes(msg);
            if (buffer == null || buffer.Length == 0) return false;
            return await client.Send(buffer);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
