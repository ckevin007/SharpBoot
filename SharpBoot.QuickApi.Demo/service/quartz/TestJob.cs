using log4net;
using Newtonsoft.Json;
using Quartz;
using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Log4net;
using SharpBoot.Starter.Quartz.Attributes;
using SharpBoot.Starter.Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.Demo.service.quartz
{
    [QuartzJob("/2 * * * * ?", "test-job")]
    //[Component]
    //[DisallowConcurrentExecution] //防止产生多个实例
    public class TestJob : IJob
    {
        [Value("test:name")] string name;
        [Autowired] QuartzJobStarter jobStarter;
        private readonly ILog log = LogFactory.GetLogger<TestJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            var param = context.JobDetail.JobDataMap;
            log.Info($"execute name={name} {JsonConvert.SerializeObject(param)}");

        }
    }
}
