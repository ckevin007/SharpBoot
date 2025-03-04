using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentScanAttribute : Attribute
    {
        public string[] Assemblys { get; }

        public ComponentScanAttribute(params string[] assemblys)
        {
            this.Assemblys = assemblys;
        }
    }
}
