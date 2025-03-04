using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValueAttribute : Attribute
    {
        public string Name { get; set; }
        public ValueAttribute(string name)
        {
            Name = name;
        }

    }
}
