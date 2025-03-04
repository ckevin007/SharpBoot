using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Nacos.V2;
using Nacos.V2.Naming.Dtos;
using RabbitMQ.Helper.Models;
using SharpBoot.Auth.extension;
using SharpBoot.Auth.filters;
using SharpBoot.Common.Attributes;
using SharpBoot.QuickApi.Demo.model;
using SharpBoot.QuickApi.model;
using SharpBoot.Starter.Nacos.Attributes;
using SharpBoot.Starter.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValueController : ControllerBase
    {
        [Value("test:name-1")] string name;
        [Value("test:name")] public string Property { get; set; }
        [NacosValue("test:name")] string nacosName;
        [NacosValue("sharpboot:quick-api:rabbitmq")] ConnectionInfo rabbitConnection;
        [Autowired] UserInfo userInfo;
        [Autowired] INacosNamingService nameingService;

        [HttpGet("/index")]
        [Auth("admin")]
        public Result Index()
        {
            return Result.Ok();
            //var instance = nameingService.SelectOneHealthyInstance("SharpBoot.Demo").Result;
            //var host = $"{instance.Ip}:{instance.Port}";

            //var baseUrl = instance.Metadata.TryGetValue("secure", out _)
            //    ? $"https://{host}"
            //    : $"http://{host}";

            //var cache = RedisHelper.Get("inject");

            //var user = HttpContext.GetUser<UserVo>();
            //return Result.Object(new
            //{
            //    Name = name,
            //    Property = Property,
            //    NacosName = nacosName,
            //    userInfo,
            //    cache,
            //    baseUrl,
            //    rabbitConnection,
            //    user = user
            //});
        }

    }
}
