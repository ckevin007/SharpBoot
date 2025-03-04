using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Models;

namespace SharpBoot.Demo.Config
{
    [Import(typeof(UserInfo), LifeTime = Common.Enums.ComponentLifeTime.Singleton)]
    [Component]
    public class ImportConfig
    {
    }
}
