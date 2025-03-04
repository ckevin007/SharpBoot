
using SharpBoot.QuickApi.Demo.model;
using SharpBoot.QuickApi.model;
using SharpBoot.Starter.WebApiClient.attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;


namespace SharpBoot.QuickApi.Demo.service.rmi
{
    [WebApi("Rmi:Auth")]
    //[HttpApiLogFilter]
    public interface IAuthApi : IHttpApi
    {
        [HttpPost("/auth/login")]
        [JsonReturn]
        ITask<Result> Login([JsonContent] LoginParam param);

        [HttpGet("/auth/identify")]
        [JsonReturn]
        ITask<Result> Identify(string token);
    }
}
