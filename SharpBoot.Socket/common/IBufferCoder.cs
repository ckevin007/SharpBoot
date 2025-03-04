using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.common
{
    public interface IBufferCoder<T> : IDisposable where T : IChannelMsg
    {

        event Action<T> NewMsg;

        byte[] Encode(T t);

        void Decode(byte[] buffer);
    }
}
