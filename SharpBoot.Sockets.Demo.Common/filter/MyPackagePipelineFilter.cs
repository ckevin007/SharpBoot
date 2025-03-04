using SharpBoot.Sockets.Demo.Common.model;
using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.Demo.Common.filter
{
    public class MyPackagePipelineFilter : FixedHeaderPipelineFilter<MyPackageInfo>
    {

        private byte[] cache = null;

        public MyPackagePipelineFilter() : base(4)
        {
        }

        protected override MyPackageInfo DecodePackage(ref ReadOnlySequence<byte> s)
        {
            var buffer = s.ToArray();

            return new MyPackageInfo()
            {
                BodyLength = buffer.Length - 4,
                Body = buffer.AsSpan()[4..(buffer.Length)].ToArray()
            };
        }

        protected override int GetBodyLengthFromHeader(ref ReadOnlySequence<byte> buffer)
        {
            var bodyLength = BitConverter.ToInt32(buffer.ToArray(), 0);
            return bodyLength;
        }
    }
}
