using System.Collections.Generic;
using System;
using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Swagger;
using SharpBoot.Starter.Quartz.Attributes;
using SharpBoot.Demo.Models;
using SharpBoot.Starter.Redis;
using SharpBoot.Starter.Caching;
using SharpBoot.Starter.Freesql;
using SharpBoot.Starter.RabbitMQ.attribute;
using SharpBoot.Starter.WebApiClient.attribute;
using SharpBoot.Demo.QuartzWorker;
using Quartz.Impl;
using Quartz;
using System.Threading.Tasks;
using log4net;
using Org.BouncyCastle.Utilities;
using SharpBoot.Repository;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;

namespace SharpBoot.Demo
{
    [EnableWebApiClient]
    [EnableFreesql]
    [EnableCaching]
    //[EnableRedis]
    [EnableQuartz]
    [EnableSwagger]
    [EnableRabbitMQ]
    public class Program
    {
        static void Main(string[] args)
        {


            //new Test().Start();
            //Console.ReadLine();

            SharpBootApplication.Run<Program>(args);
        }
    }


    public class Test
    {
        public async Task Start()
        {
            StdSchedulerFactory stdsf = new StdSchedulerFactory();
            var scheduler = await stdsf.GetScheduler();
            scheduler.JobFactory = new UJobFactory();
            await scheduler.Start();
            string id = Guid.NewGuid().ToString();
            IJobDetail job = JobBuilder.Create<TestJob>()
                 .WithIdentity(id, id)
                 .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(id, id)
                 .StartNow()
                .WithCronSchedule("0/1 * * * * ?")
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }





    public class TestJob : IJob
    {
        private static int times = 0;

        private static IFreeSql fsql;


        public static IFreeSql Fsql
        {
            get
            {
                if (fsql == null)
                {
                    fsql = FreeSql();
                }
                return fsql;
            }
        }
        public static IFreeSql FreeSql()
        {
            IFreeSql sql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, "server=123.207.31.37;port=3306;database=freesql_test;uid=root;password=Mei19931129;sslmode=none")
                .UseAutoSyncStructure(true)
                .Build();

            return sql;
        }

        private UserRepository repo;

        public Task Execute(IJobExecutionContext context)
        {
            IFreeSql fsql = Fsql;
            Console.WriteLine($"朕执行了  {++times}");

            repo = new UserRepository(fsql, new UnitOfWorkManager(fsql));
            return Task.CompletedTask;
        }
    }

    public class UJobFactory : IJobFactory
    {

        public UJobFactory()
        {

        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = new TestJob();

            return job;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable == null) return;
            disposable.Dispose();
        }
    }

}
