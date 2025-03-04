using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpBoot.Sockets.common.buffer_utils
{
    public static class ThuBufferUtils
    {
        public static T ToObject<T>(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return default(T);
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            T t = Activator.CreateInstance<T>();
            int readIndex = 0;
            foreach (var itm in ps)
            {
                ThuBufferAttribute bufferAttribute = itm.GetCustomAttribute<ThuBufferAttribute>();
                if (bufferAttribute == null) continue;
                switch (bufferAttribute.BufferType)
                {
                    case ThuBufferType.Bool:
                        itm.SetValue(t, BitConverter.ToBoolean(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.String:
                        {
                            string attrName = bufferAttribute.ByteArrayLengthPropertyName;
                            int prepareLength = bufferAttribute.Length;
                            if (prepareLength <= 0)
                            {
                                var property = ps.Where(a => a.Name == attrName).FirstOrDefault();
                                prepareLength = GetPropertyIntValue(property, t);
                                if (bufferAttribute.ByteArrayLengthTimes > 0)
                                {
                                    prepareLength = (int)(prepareLength * bufferAttribute.ByteArrayLengthTimes);
                                }
                            }

                            string target = StringExtension.FromBytes(buffer, readIndex, prepareLength);
                            itm.SetValue(t, target);
                            readIndex += prepareLength;
                        }
                        break;
                    case ThuBufferType.Short:
                        itm.SetValue(t, BitConverter.ToInt16(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.UShort:
                        itm.SetValue(t, BitConverter.ToUInt16(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.Int:
                        itm.SetValue(t, BitConverter.ToInt32(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.UInt:
                        itm.SetValue(t, BitConverter.ToUInt32(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.Long:
                        itm.SetValue(t, BitConverter.ToInt64(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.ULong:
                        itm.SetValue(t, BitConverter.ToUInt64(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.Float:
                        itm.SetValue(t, BitConverter.ToSingle(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.Double:
                        itm.SetValue(t, BitConverter.ToDouble(buffer, readIndex));
                        readIndex += bufferAttribute.Length;
                        break;
                    case ThuBufferType.Byte:
                        itm.SetValue(t, buffer[readIndex]);
                        readIndex += 1;
                        break;
                    case ThuBufferType.ByteArray:
                        {
                            string attrName = bufferAttribute.ByteArrayLengthPropertyName;
                            int prepareLength = bufferAttribute.Length;
                            if (prepareLength <= 0)
                            {
                                var property = ps.Where(a => a.Name == attrName).FirstOrDefault();
                                if (property != null)
                                {
                                    int length = GetPropertyIntValue(property, t);
                                    if (bufferAttribute.ByteArrayLengthTimes > 0)
                                    {
                                        length = (int)(length * bufferAttribute.ByteArrayLengthTimes);
                                    }
                                    if (length > 0)
                                    {
                                        if (buffer.Length - readIndex < length)
                                        {
                                            length = buffer.Length - readIndex;
                                        }
                                        if (length > 0)
                                        {
                                            byte[] tmpBuffer = buffer.AsSpan().Slice(readIndex, length).ToArray();
                                            itm.SetValue(t, tmpBuffer);
                                            readIndex += length;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                byte[] tmpBuffer = buffer.AsSpan().Slice(readIndex, prepareLength).ToArray();
                                itm.SetValue(t, tmpBuffer);
                                readIndex += prepareLength;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return t;
        }
        public static byte[] GetBytes<T>(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            byte[] buffer = new byte[0];
            foreach (var itm in ps)
            {
                ThuBufferAttribute bufferAttribute = itm.GetCustomAttribute<ThuBufferAttribute>();
                if (bufferAttribute == null) continue;
                byte[] itmBuffer = null;
                switch (bufferAttribute.BufferType)
                {
                    case ThuBufferType.Bool:
                        itmBuffer = BitConverter.GetBytes((bool)itm.GetValue(t));
                        break;
                    case ThuBufferType.String:
                        {
                            string attrName = bufferAttribute.ByteArrayLengthPropertyName;
                            int prepareLength = bufferAttribute.Length;
                            if (prepareLength <= 0)
                            {
                                var property = ps.Where(a => a.Name == attrName).FirstOrDefault();
                                prepareLength = GetPropertyIntValue(property, t);
                                if (bufferAttribute.ByteArrayLengthTimes > 0)
                                {
                                    prepareLength = (int)(prepareLength * bufferAttribute.ByteArrayLengthTimes);
                                }
                            }
                            itmBuffer = ((string)itm.GetValue(t)).ToBytes(prepareLength);
                            if (itmBuffer.Length < prepareLength)
                            {
                                itmBuffer = itmBuffer.Concat(new byte[prepareLength - itmBuffer.Length]).ToArray();
                            }
                        }
                        break;
                    case ThuBufferType.Short:
                        itmBuffer = BitConverter.GetBytes((short)itm.GetValue(t));
                        break;
                    case ThuBufferType.Int:
                        itmBuffer = BitConverter.GetBytes((int)itm.GetValue(t));
                        break;
                    case ThuBufferType.Long:
                        itmBuffer = BitConverter.GetBytes((long)itm.GetValue(t));
                        break;
                    case ThuBufferType.Float:
                        itmBuffer = BitConverter.GetBytes((float)itm.GetValue(t));
                        break;
                    case ThuBufferType.Double:
                        itmBuffer = BitConverter.GetBytes((double)itm.GetValue(t));
                        break;
                    case ThuBufferType.ByteArray:
                        itmBuffer = (byte[])itm.GetValue(t);
                        break;
                    case ThuBufferType.Byte:
                        itmBuffer = new byte[] { (byte)itm.GetValue(t) };
                        break;
                    default:
                        break;
                }
                if (itmBuffer != null)
                {
                    buffer = buffer.Concat(itmBuffer).ToArray();
                }
            }
            return buffer;
        }

        private static int GetPropertyIntValue(PropertyInfo property, object t)
        {
            if (property.PropertyType == typeof(short)) return (short)property.GetValue(t);
            if (property.PropertyType == typeof(int)) return (int)property.GetValue(t);
            if (property.PropertyType == typeof(long)) return (int)(long)property.GetValue(t);
            if (property.PropertyType == typeof(byte)) return (byte)property.GetValue(t);
            if (property.PropertyType == typeof(string)) return int.Parse(property.GetValue(t).ToString());
            return 0;
        }
    }
}
