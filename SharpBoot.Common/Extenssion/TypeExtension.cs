using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SharpBoot.Common.Extenssions
{
    public static class TypeExtension
    {
        public static List<FieldInfo> GetFieldList(this Type type,
            bool inherit = true, BindingFlags flags = BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Default |
                BindingFlags.NonPublic)
        {

            List<FieldInfo> list = type.GetFields(flags)?.ToList();
            if (list == null) list = new List<FieldInfo>();
            if (!inherit) return list;
            Type baseType = type.BaseType;
            if (baseType != null)
            {
                list.AddRange(GetFieldList(baseType, inherit));
            }
            return list;
        }
    }
}
