using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Models;
using SharpBoot.Starter.Caching.Attributes;
using SharpBoot.Starter.Caching.Interceptors;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    //[Component]
    [Interceptor(typeof(CachingInterceptor))]
    public class CachingTest
    {
        private readonly ILog log = LogFactory.GetLogger<CachingTest>();

        [Cacheable(":cachingable:", "{id}", 3600)]
        public virtual UserInfo Get(int id)
        {
            log.Info("==========Get==========");
            return new UserInfo(id, "zed");
        }

        [Cacheput(":cachingput:", 3600, "{user.User.Name}", "{user.Id}")]
        public virtual UserInfo Put(UserInfo user)
        {

            return user;
        }

        [Cachedel(":cachingput:", "{user.Id}", "{user.User.Name}", "{user.Name}")]
        public virtual void Del(UserInfo user)
        {

        }
    }
}
