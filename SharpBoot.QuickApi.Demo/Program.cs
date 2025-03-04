using SharpBoot.Auth;
using SharpBoot.QuickApi.attribute;
using SharpBoot.Starter.Caching;
using SharpBoot.Starter.Freesql;
using SharpBoot.Starter.Log4net.DashBoard.Attributes;
using SharpBoot.Starter.Nacos.Attributes;
using SharpBoot.Starter.Quartz.Attributes;
using SharpBoot.Starter.RabbitMQ.attribute;
using SharpBoot.Starter.Redis;
using SharpBoot.Starter.WebApiClient.attribute;
using System;

namespace SharpBoot.QuickApi.Demo
{
    [EnableSharpBootQuickApi]
    //[EnableNacos]
    //[EnableRedis]
    //[EnableRabbitMQ]
    //[EnableQuartz]
    //[EnableFreesql]
    //[EnableCaching]
    [EnableWebApiClient]
    [EnableSharpBootAuth]
    public class Program
    {
        public static void Main(string[] args)
        {
            SharpBootApplication.Run<Program>(args);
        }
    }
}
