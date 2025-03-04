using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PostConstructAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
