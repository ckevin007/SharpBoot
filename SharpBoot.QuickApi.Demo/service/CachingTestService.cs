using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Starter.Caching.Attributes;
using SharpBoot.Starter.Caching.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.Demo.service
{
    // [Component(ComponentLifeTime.Scoped)]
    [Interceptor(typeof(CachingInterceptor))]
    public class CachingTestService
    {
        [Cacheable(":caching:test", "", 60)]
        public virtual TestVo Stat()
        {
            return new TestVo()
            {
                Id = 18,
                Name = "Yasuo"
            };
        }
    }


    public class TestVo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
