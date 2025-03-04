using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.common.buffer_utils
{
    public class ThuBufferAttribute : Attribute
    {
        public ThuBufferType BufferType { get; set; }
        public int Length { get; set; }
        public string ByteArrayLengthPropertyName { get; set; }
        public double ByteArrayLengthTimes { get; set; }
        public bool NeedByteArrayReverse { get; set; }


        public ThuBufferAttribute(ThuBufferType bufferType, int length = 0)
        {
            BufferType = bufferType;
            Length = length;
            int getLength = GetLength(bufferType);
            if (getLength != 0) Length = getLength;

        }



        private int GetLength(ThuBufferType bufferType)
        {
            switch (bufferType)
            {
                case ThuBufferType.Byte:
                    return 1;
                case ThuBufferType.Bool:
                    return 1;
                case ThuBufferType.Short:
                    return 2;
                case ThuBufferType.UShort:
                    return 2;
                case ThuBufferType.Int:
                    return 4;
                case ThuBufferType.UInt:
                    return 4;
                case ThuBufferType.Long:
                    return 8;
                case ThuBufferType.ULong:
                    return 8;
                case ThuBufferType.Float:
                    return 4;
                case ThuBufferType.Double:
                    return 8;
                case ThuBufferType.ByteArray:
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
