using SharpBoot.Common.Enums;
using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 按照名称精准注入时，应设定
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 一个接口有多个实现类时，设定主要实现类
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// 组件生命周期类型
        /// </summary>
        public ComponentLifeTime LifeTime { get; set; }

        public ComponentAttribute(ComponentLifeTime lifeTime = ComponentLifeTime.Singleton)
        {
            this.LifeTime = lifeTime;
        }

    }
}
