using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Services.impl
{
    [Component]
    public class OrderParentConfig
    {
        [Bean(Name = "Parent-01")]
        public IOrderParent Parent1()
        {
            return new OrderParentFirst();
        }

        [Bean(Name = "Parent-02")]
        public IOrderParent Parent2()
        {
            return new OrderParentSecond();
        }
    }
}
