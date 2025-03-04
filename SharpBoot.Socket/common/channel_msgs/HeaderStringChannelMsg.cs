using SharpBoot.Sockets.common.buffer_utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.common.channel_msgs
{
    public class HeaderStringChannelMsg : IChannelMsg
    {
        public static readonly string Flag = "#";

        [ThuBuffer(ThuBufferType.String, 1)]
        public string FlagString { get; set; } = Flag;

        [ThuBuffer(ThuBufferType.Int)]
        public int BodyLength { get; set; }

        [ThuBuffer(ThuBufferType.ByteArray, ByteArrayLengthPropertyName = "BodyLength")]
        public byte[] BodyBuffer { get; set; }


        public static HeaderStringChannelMsg Build(byte[] body)
        {
            return new HeaderStringChannelMsg()
            {
                BodyBuffer = body,
                BodyLength = body == null ? 0 : body.Length
            };
        }
    }
}
