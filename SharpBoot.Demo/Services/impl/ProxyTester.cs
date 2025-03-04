using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.impl
{
    [Component]
    [Interceptor(typeof(TestInterceptor))]
    public class ProxyTester
    {
        public virtual void Test()
        {
            Hander();
        }

        public virtual void Hander()
        {
            Hander2();
        }

        public virtual void Hander2()
        {

        }
    }
}
