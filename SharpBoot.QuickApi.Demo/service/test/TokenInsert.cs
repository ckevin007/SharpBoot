using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.QuickApi.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.service.test
{
    // [Component]
    public class TokenInsert : IApplicationRunner
    {
        [Autowired] TokenService tokenService;
        public void Run(string[] args = null)
        {
            //var token = tokenService.GetOrNewToken(1);
        }
    }
}
