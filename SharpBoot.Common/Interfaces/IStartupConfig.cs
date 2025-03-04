using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SharpBoot.Common.Interfaces
{
    public interface IStartupConfig
    {

        void Configure(IApplicationBuilder app);
        void ConfigureServices(IServiceCollection services);


    }
}
