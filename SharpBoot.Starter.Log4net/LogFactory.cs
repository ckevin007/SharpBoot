using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net.Config;
using log4net.Repository;
using log4net;

namespace SharpBoot.Starter.Log4net
{
    public class LogFactory
    {
        private static ILog logger;
        private static bool isShowLogOnConsole = false;
        private static ILoggerRepository repository;
        static LogFactory()
        {
            if (logger == null)
            {
                repository = LogManager.CreateRepository("NETCoreRepository");
                //log4net从log4net.config文件中读取配置信息
                FileInfo fileinfo = new FileInfo(Path.Combine("XML", "log4net-config.xml"));

                if (fileinfo.Exists)
                {
                    XmlConfigurator.Configure(repository, fileinfo);
                }

                logger = LogManager.GetLogger(repository.Name, "InfoLogger");
            }
        }

        public static ILog GetLogger<T>()
        {
            return LogManager.GetLogger(repository.Name, typeof(T));
        }
        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(repository.Name, type);
        }


        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(string message, Exception exception = null)
        {

            if (exception == null)
            {
                //if(isShowLogOnConsole) Console.WriteLine(message);
                logger.Info(message);
            }
            else
            {
                // if (isShowLogOnConsole) Console.WriteLine(message);
                // if (isShowLogOnConsole) Console.WriteLine(exception.ToString());
                logger.Info(message, exception);
            }

        }

        /// <summary>
        /// 告警日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Warn(message);
            else
                logger.Warn(message, exception);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Error(message);
            else
                logger.Error(message, exception);
        }
    }
}
