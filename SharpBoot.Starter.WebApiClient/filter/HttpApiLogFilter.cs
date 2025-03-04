using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace SharpBoot.Starter.WebApiClient.filter
{
    public class HttpApiLogFilterAttribute : ApiActionFilterAttribute
    {

        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            Console.WriteLine($"[rmi] request {context.ApiActionDescriptor.Name} {context.RequestMessage.RequestUri}");
            return base.OnBeginRequestAsync(context);
        }
        public override Task OnEndRequestAsync(ApiActionContext context)
        {
            return base.OnEndRequestAsync(context);
        }
    }
}
