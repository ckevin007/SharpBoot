using SharpBoot.Common.Attributes;
using SharpBoot.Sockets.Demo.Common.filter;
using SharpBoot.Sockets.Demo.Common.model;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Sockets.Demo.Server.tcpserver
{
    [Component]
    public class MyPipelineFilterFactory : IPipelineFilterFactory<MyPackageInfo>
    {
        [Autowired] IServiceProvider serviceProvider;
        public IPipelineFilter<MyPackageInfo> Create(object client)
        {
            return new MyPackagePipelineFilter();
        }
    }
}
