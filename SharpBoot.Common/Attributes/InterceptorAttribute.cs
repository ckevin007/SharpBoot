using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InterceptorAttribute : Attribute
    {
        public Type[] InterceptorTypes { get; set; }
        public InterceptorAttribute(params Type[] interceptorTypes)
        {
            InterceptorTypes = interceptorTypes;
        }

    }
}
