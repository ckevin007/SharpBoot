using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Starter.Log4net;

namespace SharpBoot.Demo.Filters
{
    public class TestFilter2 : IResultFilter
    {
        private readonly ILog log = LogFactory.GetLogger<TestFilter>();
        public void OnResultExecuted(ResultExecutedContext context)
        {
            log.Info("OnResultExecuted  2");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            log.Info("OnResultExecuting  2");
        }
    }
}
