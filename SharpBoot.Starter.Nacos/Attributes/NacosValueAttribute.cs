using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Nacos.Attributes
{
    public class NacosValueAttribute : ValueAttribute
    {
        public NacosValueAttribute(string name) : base(name)
        {
        }
    }
}
