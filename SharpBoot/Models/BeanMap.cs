using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Models
{
    public static class BeanMap
    {
        public static List<BeanInfo> BeanList { get; set; } = new List<BeanInfo>();

        private static object locker = new object();

        public static void Add(BeanInfo bean)
        {
            lock (locker)
            {
                BeanList.Add(bean);
            }
        }

        public static void Remove(BeanInfo bean)
        {
            lock (locker)
            {
                BeanList.Remove(bean);
            }
        }

        public static List<BeanInfo> Get(Type type)
        {
            lock (locker)
            {
                return BeanList.Where(a => a.ObjType.IsAssignableFrom(type)).ToList();
            }
        }
        public static BeanInfo First(Type type, string name)
        {
            return Get(type)?.Where(a => a.Name == name)?.FirstOrDefault();
        }
    }
}
