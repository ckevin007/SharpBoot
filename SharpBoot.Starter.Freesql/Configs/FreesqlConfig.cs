using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Freesql.Configs
{
    // [Component]
    public class FreesqlConfig : IStartupConfig
    {
        //[Bean(ComponentLifeTime.Transient)]
        //public UnitOfWorkManager Uowm(IFreeSql fsql)
        //{
        //    if (fsql == null) return null;
        //    return new UnitOfWorkManager(fsql);
        //}
        public void Configure(IApplicationBuilder app)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
