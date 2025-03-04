using log4net;
using Quartz;
using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Quartz.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Starter.Log4net;
using SharpBoot.Repository;
using SharpBoot.Entity;
using SharpBoot.Common.Enums;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace SharpBoot.Demo.QuartzWorker
{
    //[QuartzJob("0/1 * * * * ?")]
    //[Component(ComponentLifeTime.Transient)]
    //[DisallowConcurrentExecution]
    public class UserQuartzWorker : IJob  //, IDisposable
    {
        //private readonly ILog log = LogFactory.GetLogger<UserQuartzWorker>();

        [Autowired] UserRepository userRepo;
        [Autowired] Yasuo suoer;


        private static int times = 0;

        public void Dispose()
        {

        }


        public async Task Execute(IJobExecutionContext context)
        {

            // List<User> list = await userRepo.Select.ToListAsync();


            // log.Info($"朕执行了 list.Count={list.Count}");
            Console.WriteLine($"朕执行了  {++times}");
        }
    }

    [Component(ComponentLifeTime.Transient)]
    public class Yasuo
    {

    }
}
