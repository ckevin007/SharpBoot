using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Nacos.Attributes;
using SharpBoot.Starter.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.config
{
    //[Component]
    public class RedisInjectConfig
    {
        [NacosValue("sharpboot:quick-api:redis")] RedisConfig redisConfig;

        [Bean]
        public RedisConfig GetRedisConfig()
        {
            return this.redisConfig;
        }
    }
}
