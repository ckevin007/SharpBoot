using Nacos.V2;
using Newtonsoft.Json;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Services;
using SharpBoot.Starter.Nacos.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Nacos.ValueConfigurations
{
    [Component]
    public class NacosValueConfiguration : IValueInjecter
    {
        public Type ValueAttributeType => typeof(NacosValueAttribute);

        public Type ConfigPropertyType => typeof(NacosConfigPropertyAttribute);

        [Autowired] INacosConfigService configService;

        [Value("Nacos:GroupName")] string defaultGroupName;

        public T Get<T>(string section)
        {
            return (T)Get(typeof(T), section);
        }

        public object Get(Type valueType, string section)
        {
            var dataId = section;
            var group = defaultGroupName;
            if (configService == null) return default;
            var config = configService.GetConfig(dataId, group, 5000L).Result;
            if (string.IsNullOrEmpty(config)) return default;
            if (valueType.IsAssignableFrom(config.GetType()))
            {
                return config;
            }
            else
            {
                if (valueType.IsValueType) return Convert.ChangeType(config, valueType);
                var result = JsonConvert.DeserializeObject(config, valueType);
                return result;
            }
        }



    }
}
