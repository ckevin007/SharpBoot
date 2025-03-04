using System;

namespace SharpBoot.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class AutowiredAttribute : Attribute
    {
        /// <summary>
        /// 按照名称注入
        /// </summary>
        public string Name { get; set; }


        public AutowiredAttribute(string name = "")
        {
            Name = name;
        }




    }
}
