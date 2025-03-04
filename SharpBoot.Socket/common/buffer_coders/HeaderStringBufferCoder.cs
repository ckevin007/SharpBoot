using SharpBoot.Sockets.common.buffer_utils;
using SharpBoot.Sockets.common.channel_msgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBoot.Sockets.common.buffer_coders
{
    public class HeaderStringBufferCoder : IBufferCoder<HeaderStringChannelMsg>
    {
        public event Action<HeaderStringChannelMsg> NewMsg;

        private byte[] cache;

        private readonly int flagLength = 1;
        private readonly int headLength = 5;

        public void Decode(byte[] buffer)
        {
            if (buffer == null) return;
            if (cache == null)
            {
                cache = new byte[buffer.Length];
                Array.Copy(buffer, 0, cache, 0, buffer.Length);
                string flag = Encoding.UTF8.GetString(cache, 0, flagLength);
                if (flag != HeaderStringChannelMsg.Flag)
                {
                    cache = null;
                    return;
                }
            }
            else
            {
                cache = cache.Concat(buffer).ToArray();
                buffer = null;
            }

            while (cache != null && cache.Length >= headLength)
            {
                int bodyLength = BitConverter.ToInt32(cache, flagLength);
                if (cache.Length < bodyLength + headLength)
                {
                    return;
                }
                var totalPackageLength = bodyLength + headLength;
                if (cache.Length >= totalPackageLength)
                {
                    byte[] body = cache.AsSpan()[0..totalPackageLength].ToArray();
                    if (cache.Length > totalPackageLength)
                    {
                        cache = cache.AsSpan()[totalPackageLength..cache.Length].ToArray();
                    }
                    else
                    {
                        cache = null;
                    }
                    HeaderStringChannelMsg msg = ThuBufferUtils.ToObject<HeaderStringChannelMsg>(body);
                    NewMsg(msg);
                }
            }
        }

        public byte[] Encode(HeaderStringChannelMsg t)
        {
            return ThuBufferUtils.GetBytes(t);
        }

        public void Dispose()
        {
            cache = null;
        }


    }
}
