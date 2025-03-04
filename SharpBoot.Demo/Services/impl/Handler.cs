using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Models;

namespace SharpBoot.Demo.Services.impl
{
    [Component(Name = "Handler")]
    public class Handler : IHandler<UserInfo>, IOrderService
    {
        public void TestOrder()
        {

        }
    }

    [Component(Name = "Handler2")]
    public class Handler2 : IHandler<UserInfo>, IOrderService
    {
        public void TestOrder()
        {

        }
    }
}
