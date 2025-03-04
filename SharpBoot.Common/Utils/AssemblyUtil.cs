using SharpBoot.Common.Attributes;
using SharpBoot.Common.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpBoot.Common.Utils
{
    public static class AssemblyUtil
    {
        public static List<Assembly> GetSharpBootApplicationAssemblys<T>()
        {
            return GetSharpBootApplicationAssemblys(typeof(T));
        }
        public static List<Assembly> GetSharpBootApplicationAssemblys(Type startType, params Assembly[] assemblys)
        {
            var assemblyList = new List<Assembly>
            {
                startType.Assembly
            };
            if (assemblys != null && assemblys.Length > 0) assemblyList.AddRange(assemblys);
            List<ComponentScanAttribute> componentScans = AttributeExtension.GetAttributes<ComponentScanAttribute>(startType, true);
            List<string> assemblyNames = new List<string>();
            foreach (var itm in componentScans)
            {
                if (itm.Assemblys != null && itm.Assemblys.Length > 0)
                {
                    assemblyNames.AddRange(itm.Assemblys);
                }
            }
            if (assemblyNames == null || assemblyNames.Count == 0)
            {
                assemblyList.Add(Assembly.GetCallingAssembly());
                assemblyList.Add(Assembly.GetExecutingAssembly());
                assemblyList.Add(Assembly.GetEntryAssembly());
            }
            else
            {
                assemblyNames.ForEach(a => assemblyList.Add(Assembly.Load(a)));
            }

            assemblyList = assemblyList?.GroupBy(a => a).Select(a => a.Key).ToList();
            return assemblyList;
        }
    }
}
