using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.service
{
    //   [Component]
    public class CachingTest : IApplicationRunner
    {
        [Autowired] CachingTestService service;
        public void Run(string[] args = null)
        {
            var vo = service.Stat();

        }
    }
}
