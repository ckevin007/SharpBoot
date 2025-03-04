using log4net.Appender;
using log4net.Core;
using SharpBoot.Starter.Log4netDashBoard.Demo.service;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Log4net.DashBoard.appender
{
    public class MyMemAppender : AppenderSkeleton
    {
        public long CacheSize { get; set; } = 1000;

        private bool isFirst = true;

        protected override void Append(LoggingEvent logEvent)
        {
            if (isFirst)
            {
                isFirst = false;
                LogCacher.Instance.CacheSize = CacheSize;
            }
            string loggerName = "default";
            if (!string.IsNullOrEmpty(logEvent.LoggerName))
            {
                var array = logEvent.LoggerName.Split(".");
                loggerName = array[array.Length - 1];
            }
            string str = $"[{logEvent.TimeStamp:yyyy-MM-dd HH:mm:ss,fff}] [{loggerName}] {logEvent.Level} {logEvent.RenderedMessage}";
            LogCacher.Instance.AddLog(str);
        }
    }
}
