using SharpBoot.Auth.model;
using SharpBoot.Auth.service;
using SharpBoot.Common.Attributes;
using SharpBoot.QuickApi.Demo.model;
using SharpBoot.QuickApi.Demo.service.rmi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.Demo.service
{
    [Component(Primary = true)]
    [Order(int.MinValue)]
    public class TokenService : ITokenService
    {
        [Autowired] IAuthApi api;
        public string GetOrNewToken(int usrId)
        {
            throw new NotImplementedException();
        }

        public async Task<IUserVo> GetUserByToken(string token)
        {
            try
            {
                var rt = await api.Identify(token);
                if (rt.Code != 0) return null;
                return rt.Parse<UserVoBase>();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public void RemoveToken(int usrId)
        {
            throw new NotImplementedException();
        }
    }
}
