using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.model
{
    /// <summary>
    /// 隐藏swagger接口特性标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HiddenApiAttribute : System.Attribute
    {
    }
}
