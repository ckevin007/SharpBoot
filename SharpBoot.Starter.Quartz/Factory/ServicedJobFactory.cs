using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Quartz.Factory
{
    [Component]
    public class ServicedJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;
        public ServicedJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            if (job is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
