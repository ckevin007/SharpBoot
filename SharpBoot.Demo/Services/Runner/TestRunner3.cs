using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Models;
using System;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.Runner
{
    [Component]
    public class TestRunner3 : IApplicationRunner
    {
        [Value("Urls")] string Urls;
        public TestRunner3(UserInfo user)
        {

        }
        //[Autowired] UserInfo user;
        public void Run(string[] args = null)
        {
            Console.WriteLine("TestRunner3.Run");
        }

        [PostConstruct(Order = 2)]
        public Task Init(UserInfo user)
        {
            Console.WriteLine("TestRunner3.Init");
            return Task.CompletedTask;
        }

        [PostConstruct(Order = 1)]
        public void Init2()
        {
            Console.WriteLine("TestRunner3.Init2");
        }
    }
}
