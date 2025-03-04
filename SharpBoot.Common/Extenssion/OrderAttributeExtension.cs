using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;

namespace SharpBoot.Common.Extenssions
{
    public static class OrderAttributeExtension
    {
        public static int Order(Type type)
        {
            OrderAttribute order = type.GetCustomAttribute<OrderAttribute>(true);
            if (order == null) return int.MaxValue;
            return order.Value;
        }
    }
}
