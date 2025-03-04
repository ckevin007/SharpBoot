using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheableAttribute : Attribute
    {
        public CacheableAttribute(string keyName, string keyParam, int expSecond = 0)
        {
            KeyName = keyName;
            KeyParam = keyParam;
            ExpSecond = expSecond;
        }

        public string KeyName { get; set; }

        public string KeyParam { get; set; }

        public int ExpSecond { get; set; } = 0;


    }
}
