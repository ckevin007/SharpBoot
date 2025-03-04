using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using IFreesqlDemo.Services;
using Microsoft.AspNetCore.Mvc;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Models;

namespace SharpBoot.Demo.Services.Runner
{
    // [Component]
    [Order(0)]
    public class TestRunner2 : IApplicationRunner
    {
        [Autowired]
        private UserInfo user;

        [Autowired]
        private IOrderParent orderService;

        [Autowired]
        private IHandler<UserInfo> handler;

        [Autowired]
        private List<IUserService> userServices;

        [Autowired]
        private UserService userService;

        [Autowired(Name = "Parent-01")]
        private IOrderParent orderParent;

        [Autowired(Name = "Handler")]
        private IOrderParent orderParentFromAtor;

        public TestRunner2([Autowired(Name = "Parent-01")] IOrderParent orderParent)
        {

        }

        public void Run(string[] args = null)
        {
            //var user = userService.TranscationalTest().Result;

            bool flag = orderParent == orderParentFromAtor;
        }
    }
}
