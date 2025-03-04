using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Redis
{

    [ConfigProperty("Redis")]
    public class RedisConfig
    {
        public string Configname { get; set; }
        public string Connection { get; set; }
        public int DefaultDatabase { get; set; }
        public string InstanceName { get; set; }
    }
}
