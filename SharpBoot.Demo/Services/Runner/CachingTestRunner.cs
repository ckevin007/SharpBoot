using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Models;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    // [Component]
    public class CachingTestRunner : IApplicationRunner
    {
        [Autowired]
        CachingTest test;

        private readonly ILog log = LogFactory.GetLogger<CachingTestRunner>();

        public void Run2(string[] args = null)
        {
            UserInfo u = new UserInfo(456, "yasuo");
            u.User = new UserInfo(123, "zed");
            var hh = test.Get(123);
            log.Info($"测试结果 {hh}");
        }

        public void Handle(UserInfo user)
        {
            user = new UserInfo(789456, "zed-11");
        }

        public void Run(string[] args = null)
        {
            UserInfo u = new UserInfo(456, "yasuo");
            u.User = new UserInfo(123, "zed");
            Handle(u);
            log.Info($"测试结果 {u}");
        }
    }
}
