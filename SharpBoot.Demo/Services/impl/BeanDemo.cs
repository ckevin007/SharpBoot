using IFreesqlDemo.Services;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Demo.Services.impl
{
    public class BeanDemo
    {
        [Value("User")]
        public UserInfo User { get; set; }

        [Value("User")]
        private UserInfo user;

        [Autowired]
        private UserService userService;

        public BeanDemo()
        {
            Console.WriteLine("BeanDemo-->ctor");
        }
        public void Say()
        {
            Console.WriteLine($"this is BeanDemo say User.Name={User.Name}");
        }
    }
}
