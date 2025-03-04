using SharpBoot.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BeanAttribute : Attribute
    {
        /// <summary>
        /// 组件生命周期类型
        /// </summary>
        public ComponentLifeTime LifeTime { get; set; }

        /// <summary>
        /// 按照名称精准注入时，应设定
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 一个接口有多个实现类时，设定主要实现类
        /// </summary>
        public bool Primary { get; set; }

        public Type ReturnType { get; set; }

        public BeanAttribute(ComponentLifeTime lifeTime = ComponentLifeTime.Singleton)
        {
            this.LifeTime = lifeTime;
        }
    }
}
