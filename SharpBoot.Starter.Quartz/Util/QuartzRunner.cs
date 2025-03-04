using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SharpBoot.Starter.Log4net;
using Quartz;
using SharpBoot.Starter.Quartz.Attributes;
using System.Reflection;

namespace SharpBoot.Starter.Quartz.Util
{
    [Component]
    public class QuartzRunner : IApplicationRunner
    {
        [Autowired]
        private QuartzJobStarter starter;

        [Autowired]
        private List<IJob> jobs;

        private readonly ILog log = LogFactory.GetLogger<QuartzRunner>();

        public void Run(string[] args = null)
        {
            if (jobs == null || jobs.Count == 0) return;
            jobs.ForEach(a =>
            {
                QuartzJobAttribute attribute = a.GetType().GetCustomAttribute<QuartzJobAttribute>();
                if (attribute == null) return;
                starter.StartQuartzJob(a.GetType(), attribute.Cron, attribute.Name);
                log.Info($"定时服务注册成功,定时服务类={a.GetType().Name}");
            });
            jobs = null;
        }
    }
}
