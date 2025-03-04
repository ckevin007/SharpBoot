using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.common.buffer_utils
{
    public static class StringExtension
    {
        public static Encoding encoding = Encoding.UTF8;
        public static byte[] ToBytes(this string str, int maxLength = 0)
        {
            if (string.IsNullOrEmpty(str)) return new byte[0];
            byte[] buffer = encoding.GetBytes(str);
            if (maxLength > 0)
            {
                if (buffer.Length > maxLength)
                {
                    buffer = buffer.AsSpan().Slice(0, maxLength).ToArray();
                }
            }
            return buffer;
        }
        public static string FromBytes(byte[] buffer, int start, int length)
        {
            if (buffer == null || buffer.Length == 0) return "";
            if (buffer.Length - start < length) length = (buffer.Length - start);
            if (length <= 0) return "";
            return encoding.GetString(buffer, start, length).TrimEnd('\0');
        }
    }
}
