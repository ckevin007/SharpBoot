using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Rmi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    // [Component]
    public class TestRmiTest : IApplicationRunner
    {
        [Autowired] ITestRmi api;
        public void Run(string[] args = null)
        {
            var str = api.Test().GetAwaiter().GetResult();
            Console.WriteLine("str=" + str);
        }
    }
}
