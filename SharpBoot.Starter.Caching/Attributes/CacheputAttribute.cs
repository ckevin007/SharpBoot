using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheputAttribute : Attribute
    {
        public CacheputAttribute(string keyName, int expSecond = 0, params string[] keyParams)
        {
            KeyName = keyName;
            KeyParams = keyParams;
            ExpSecond = expSecond;
        }

        public string KeyName { get; set; }

        public string[] KeyParams { get; set; }

        public int ExpSecond { get; set; } = 0;
    }
}
