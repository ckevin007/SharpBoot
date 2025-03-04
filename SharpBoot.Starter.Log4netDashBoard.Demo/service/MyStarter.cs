using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Log4netDashBoard.Demo.service
{
    [Component]
    public class MyStarter : IApplicationRunner
    {

        private readonly ILog log = LogFactory.GetLogger<MyStarter>();
        public void Run(string[] args = null)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    log.Info("数据日志测试");
                    await Task.Delay(1000);
                }
            });
        }
    }
}
