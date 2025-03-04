using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Starter.Log4net;
using log4net;

namespace SharpBoot.Demo.Filters
{

    public class TestFilter : IResultFilter
    {
        private readonly ILog log = LogFactory.GetLogger<TestFilter>();
        public void OnResultExecuted(ResultExecutedContext context)
        {
            log.Info("OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            log.Info("OnResultExecuting");
        }
    }
}
