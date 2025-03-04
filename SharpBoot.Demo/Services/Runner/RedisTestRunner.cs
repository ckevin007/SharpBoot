using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    // [Component]
    //public class RedisTestRunner : IApplicationRunner
    //{

    //    [Autowired]
    //    private RedisConfig config;

    //    [Autowired]
    //    private RedisHelper redis;

    //    public void Run(string[] args = null)
    //    {
    //        // redis.SetCache("test:runner", 123);
    //    }
    //}


    //[Component]
    //public class RedisTest
    //{
    //    [Autowired]
    //    private RedisHelper redis;

    //    public void Test()
    //    {
    //        redis.SetCache("test:runner", 123);
    //        var value = redis.GetCache<string>("test:runner");

    //        redis.RemoveCache("test:runner");
    //        redis.SetExpire("test:runner", 30);
    //        redis.GetExpire("test:runner");
    //    }
    //}
}
