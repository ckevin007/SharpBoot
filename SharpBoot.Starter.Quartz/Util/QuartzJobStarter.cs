using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Quartz.Util
{
    [Component]
    public class QuartzJobStarter
    {
        private IScheduler scheduler = null;

        public QuartzJobStarter(IJobFactory factory)
        {
            StdSchedulerFactory stdsf = new StdSchedulerFactory();
            scheduler = stdsf.GetScheduler().Result;
            // scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            if (factory != null) scheduler.JobFactory = factory;
            scheduler.Start().Wait();
        }

        public Task StartQuartzJob<T>(string cron, string jobName = "", Dictionary<string, object> paramMap = null) where T : IJob
        {
            return StartQuartzJob(typeof(T), cron, jobName, paramMap);
        }

        public async Task StartQuartzJob(Type type, string cron, string jobName = "", Dictionary<string, object> paramMap = null)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                jobName = Guid.NewGuid().ToString();
            }
            JobKey jobKey = JobKey.Create(jobName);
            TriggerKey triggerKey = new TriggerKey(jobName);
            IJobDetail job = JobBuilder.Create(type)
                 .WithIdentity(jobKey)
                 .Build();

            if (paramMap == null) paramMap = new Dictionary<string, object>();
            paramMap["jobKey"] = jobKey;
            paramMap["triggerKey"] = triggerKey;
            job.JobDataMap.PutAll(paramMap);


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartNow()
                .WithCronSchedule(cron)
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task DeleteJob(JobKey jobKey)
        {
            await scheduler.DeleteJob(jobKey);
        }

        public async Task DeleteJob(string jobName)
        {
            await scheduler.DeleteJob(JobKey.Create(jobName));
        }

        public async Task ResumeJob(JobKey jobKey)
        {
            await scheduler.ResumeJob(jobKey);
        }
    }
}
