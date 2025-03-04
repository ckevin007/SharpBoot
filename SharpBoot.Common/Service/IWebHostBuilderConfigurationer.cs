using Microsoft.AspNetCore.Hosting;

namespace SharpBoot.Common.Service
{
    public interface IWebHostBuilderConfigurationer
    {
        void BeforeBuild(IWebHostBuilder builder);

    }
}
