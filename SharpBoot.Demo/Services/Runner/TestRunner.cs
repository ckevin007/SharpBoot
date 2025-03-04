using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using IFreesqlDemo.Services;
using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Models;
using SharpBoot.Demo.Services.impl;
using SharpBoot.Repository;
using SharpBoot.Starter.Log4net;

namespace SharpBoot.Demo.Services.Runner
{
    //[Component(Common.Enums.ComponentLifeTime.Singleton)]
    // [Order(1)]
    public class TestRunner : IApplicationRunner
    {
        [Autowired]
        private UserInfo user;

        [Autowired(Name = "Handler")]
        private IOrderParent orderService;


        [Autowired(Name = "Handler2")]
        private IOrderParent handler2;


        [Autowired(Name = "Parent-01")]
        private IOrderParent orderService1;

        [Autowired(Name = "Parent-02")]
        private IOrderParent orderService2;

        [Autowired(Name = "Parent-03")]
        private IOrderParent orderService3;

        [Autowired]
        private IHandler<UserInfo> handler;

        [Autowired]
        private List<IUserService> userServices;


        [Autowired]
        private UserService userService;

        private readonly ILog log = LogFactory.GetLogger<TestRunner>();

        [Autowired]
        private BeanComponent beanComponent;

        [Autowired]
        private ProxyTester proxytester;

        [Autowired]
        private TestInfo TestInfo;


        [Autowired] UserRepository userRepo;
        public void Run(string[] args = null)
        {

        }
    }
}
