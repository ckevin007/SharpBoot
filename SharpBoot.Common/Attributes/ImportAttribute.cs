using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Enums;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportAttribute : Attribute
    {
        public Type[] Value { get; }

        /// <summary>
        /// 组件生命周期类型
        /// </summary>
        public ComponentLifeTime LifeTime { get; set; } = ComponentLifeTime.Singleton;
        public ImportAttribute(params Type[] value)
        {
            this.Value = value;
        }
    }
}
