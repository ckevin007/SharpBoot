using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Startups
{
    // [Component]
    public class WxRequest
    {
        private readonly HttpClient client;

        [Value("WxApp:AppId")]
        string appId;

        [Value("WxApp:AppSecret")]
        string appSecret;

        public WxRequest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.weixin.qq.com");
        }

        public async Task<dynamic> Code2Session(string code)
        {
            var url = "/sns/jscode2session";
            var param = new
            {
                appid = appId,
                secret = appSecret,
                js_code = "JSCODE",
                grant_type = "authorization_code"
            };
            var obj = await client.GetAsync<dynamic>(url, param);
            return obj;
        }
    }
}
