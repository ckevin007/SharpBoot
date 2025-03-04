using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OrderAttribute : Attribute
    {
        public int Value { get; set; }

        public OrderAttribute(int value)
        {
            Value = value;
        }
    }
}
