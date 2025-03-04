using Microsoft.AspNetCore.Mvc;
using SharpBoot.Common.Attributes;
using SharpBoot.Dmeo.Apsnet.Model;
using SharpBoot.Starter.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Dmeo.Apsnet.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Autowired] UserInfo userInfo;

        [Value("test:name")] string name;

        [Autowired] RedisHelper redis;

        [HttpGet]
        public object Index()
        {
            //  redis.SetCache("test:user", "123");
            var cache = redis.GetCacheString("test:user");
            return new
            {
                userInfo,
                cache,
                name,
            };
        }
    }
}
