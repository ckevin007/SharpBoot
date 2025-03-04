using IdGen;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using System.Collections.Generic;

namespace SharpBoot.Demo.Services.Runner
{
    [Component]
    public class TestRunner4 : IApplicationRunner
    {
        [Autowired] IIdGenerator<long> Mydg;

        [Autowired] List<IUserService> MyUserServices;


        public TestRunner4(IIdGenerator<long> idg, List<IUserService> userServices)
        {

        }
        public void Run(string[] args = null)
        {

        }
    }
}
