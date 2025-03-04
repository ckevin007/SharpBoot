using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Log4net.DashBoard.Attributes
{
    [ComponentScan("SharpBoot.Starter.Log4net.DashBoard")]
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableLog4netDashBoardAttribute : Attribute
    {
    }
}
