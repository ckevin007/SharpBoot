using SharpBoot.Auth.model;
using SharpBoot.Auth.service;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.QuickApi.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.QuickApi.service
{
    [Component(Common.Enums.ComponentLifeTime.Scoped)]
    public class TokenService : ITokenService
    {
        [Autowired] ICache cache;
        private const int tokenExp = 24 * 3600;



        public async Task<IUserVo> GetUserByToken(string token)
        {
            int usrId = GetUsrIdByToken(token);
            if (usrId == 0) return null;
            RenewToken(token, usrId);
            var vo = new UserVo();
            vo.Id = usrId;
            return vo;
        }

        public string GetOrNewToken(int usrId)
        {
            string token = GetTokenByUsrId(usrId);
            if (string.IsNullOrEmpty(token))
            {
                token = Guid.NewGuid().ToString("N");
                SyncTokenUsrId(token, usrId);
            }
            else
            {
                RenewToken(token, usrId);
            }
            return token;
        }

        private void RenewToken(string token, int usrId)
        {
            cache.SetExpire($":auth:usrId:{usrId}", tokenExp);
            cache.SetExpire($":auth:token:{token}", tokenExp);
        }

        public void RemoveToken(int usrId)
        {
            string token = GetTokenByUsrId(usrId);
            if (!string.IsNullOrEmpty(token)) cache.RemoveCache($":auth:token:{token}");
            cache.RemoveCache($":auth:usrId:{usrId}");
        }


        public virtual int GetUsrIdByToken(string token)
        {
            string key = $":auth:token:{token}";
            if (!cache.Exists(key)) return 0;
            return cache.GetCache<int>(key);
        }


        public virtual string GetTokenByUsrId(int usrId)
        {
            string key = $":auth:usrId:{usrId}";
            if (!cache.Exists(key)) return "";
            return cache.GetCache<string>(key);
        }

        private void SyncTokenUsrId(string token, int usrId)
        {
            PutToken(token, usrId);
            PutUsrId(token, usrId);
        }

        public virtual void PutToken(string token, int usrId)
        {
            string key = $":auth:token:{token}";
            cache.SetCache(key, usrId, tokenExp);
        }

        public virtual void PutUsrId(string token, int usrId)
        {
            string key = $":auth:usrId:{usrId}";
            cache.SetCache(key, token, tokenExp);
        }


    }
}
