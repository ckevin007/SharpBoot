using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBoot.Sockets.Demo.Common.model
{
    public class MyPackageInfo
    {
        public int BodyLength { get; set; }

        public byte[] Body { get; set; }



        public byte[] ToBytes()
        {
            byte[] buffer = BitConverter.GetBytes(BodyLength);
            buffer = buffer.Concat(Body).ToArray();
            return buffer;
        }
    }
}
