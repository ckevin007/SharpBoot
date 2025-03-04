using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpBoot.Common.Extenssions
{
    public static class AttributeExtension
    {
        public static Type[] GetAttributeMarkTypes<T>(List<Assembly> assemblys, bool matryoshka = false) where T : Attribute
        {
            var list = new List<Type>();
            foreach (var assembly in assemblys)
            {
                var types = GetAttributeMarkTypes<T>(assembly, matryoshka);
                if (types == null || types.Length == 0) continue;
                foreach (var type in types)
                {
                    if (!list.Contains(type)) list.Add(type);
                }
            }
            return list.ToArray();
        }



        /// <summary>
        /// 扫描被某特性类标注过的type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly">程序集</param>
        /// <param name="matryoshka">是否允许"套娃"，即 特性类上再标注特性类</param>
        /// <returns></returns>
        public static Type[] GetAttributeMarkTypes<T>(Assembly assembly, bool matryoshka = false) where T : Attribute
        {
            Type[] types = assembly.GetTypes();
            var array = types.Where(a => Marked<T>(a, true)).ToArray();
            return array;
        }

        private static bool Marked<T>(Type type, bool matryoshka = false) where T : Attribute
        {
            return GetAttribute<T>(type, matryoshka) != null;
        }

        public static T GetAttribute<T>(Type type, bool matryoshka = false) where T : Attribute
        {
            return GetAttributes<T>(type, matryoshka)?.FirstOrDefault();
        }

        public static List<T> GetAttributes<T>(Type type, bool matryoshka = false) where T : Attribute
        {
            List<T> list = new List<T>();
            List<object> attributes = type.GetCustomAttributes(true)?
                .Where(a => a.GetType() != typeof(AttributeUsageAttribute))
                .Where(a => a.GetType() != typeof(AttributeTargets))
                .Where(a => a.GetType().GetCustomAttributes(true).All(c => c.GetType() != a.GetType()))
                .ToList();
            if (attributes != null && attributes.Count > 0)
            {
                T t = (T)attributes.FirstOrDefault(a => a.GetType() == typeof(T));
                if (t != null)
                {
                    list.Add(t);
                    attributes.Remove(t);
                }
            }
            if (matryoshka && attributes != null && attributes.Count > 0)
            {
                foreach (var itm in attributes)
                {
                    T t = GetAttribute<T>(itm.GetType(), matryoshka);
                    if (t != null)
                    {
                        list.Add(t);
                    }
                }
            }
            return list;
        }
    }
}
