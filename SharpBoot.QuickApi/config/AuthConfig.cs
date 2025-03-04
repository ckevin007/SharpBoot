using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Auth.model;
using SharpBoot.Auth.service;
using SharpBoot.Common.Attributes;
using SharpBoot.QuickApi.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.config
{
    [Component(Common.Enums.ComponentLifeTime.Scoped)]
    public class AuthConfig : IAuthConfig
    {

        public object NotLoginResult => Result.NotLogin();
        public object PowerErrorResult => Result.PowerError();

        [Autowired] ITokenService tokenService;


        public ITokenService GetTokenService(IServiceProvider serviceProvider)
        {
            return tokenService;
            //var tokenService = serviceProvider.GetService<ITokenService>();
            //return tokenService;
        }
    }
}
