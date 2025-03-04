using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Redis
{
    [Component]
    public class RedisStrartup : IStartupConfig
    {
        [Autowired] RedisConfig config;
        public void Configure(IApplicationBuilder app)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = $"{config.Connection},prefix={config.InstanceName},defaultDatabase={config.DefaultDatabase}";
            var csredis = new CSRedis.CSRedisClient(conn);
            RedisHelper.Initialization(csredis);
        }
    }
}
