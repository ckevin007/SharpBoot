using Castle.DynamicProxy;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Interceptors
{
    [Component]
    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(invocation.Method.Name + " invoked");
            invocation.Proceed();
        }
    }
}
