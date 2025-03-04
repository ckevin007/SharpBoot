using Microsoft.Extensions.Configuration;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Services
{
    [Component]
    public class DefalutValueInjecter : IValueInjecter
    {
        [Autowired]
        public IConfiguration Config { get; set; }

        public Type ValueAttributeType => typeof(ValueAttribute);

        public Type ConfigPropertyType => typeof(ConfigPropertyAttribute);

        public T Get<T>(string section)
        {
            return Config.GetSection(section).Get<T>();
        }

        public object Get(Type valueType, string section)
        {
            return Config.GetSection(section).Get(valueType);
        }
    }
}
