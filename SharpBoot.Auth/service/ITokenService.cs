using SharpBoot.Auth.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Auth.service
{
    public interface ITokenService
    {
        Task<IUserVo> GetUserByToken(string token);

        string GetOrNewToken(int usrId);

        void RemoveToken(int usrId);
    }
}
