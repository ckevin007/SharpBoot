using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CachedelAttribute : Attribute
    {
        public CachedelAttribute(string keyName, params string[] keyParams)
        {
            KeyName = keyName;
            KeyParams = keyParams;
        }

        public string KeyName { get; set; }

        public string[] KeyParams { get; set; }
    }
}
