using SharpBoot.Starter.WebApiClient.attribute;
using SharpBoot.Starter.WebApiClient.filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace SharpBoot.Demo.Rmi
{
    [WebApi("Rmi:Test")]
    [HttpApiLogFilter]
    public interface ITestRmi : IHttpApi
    {
        [HttpGet("/Trigger/MaterialDbUpdate")]
        ITask<string> Test();
    }
}
