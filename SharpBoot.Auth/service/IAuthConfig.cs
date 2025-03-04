using SharpBoot.Auth.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Auth.service
{
    public interface IAuthConfig
    {
        object NotLoginResult { get; }
        object PowerErrorResult { get; }

        ITokenService GetTokenService(IServiceProvider serviceProvider);
    }
}
