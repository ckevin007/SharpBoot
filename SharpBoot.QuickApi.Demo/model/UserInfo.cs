using SharpBoot.Common.Attributes;
using SharpBoot.Starter.Nacos.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.model
{
    [Component(LifeTime = Common.Enums.ComponentLifeTime.Transient)]
    [NacosConfigProperty("json:test")]
    public class UserInfo
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
