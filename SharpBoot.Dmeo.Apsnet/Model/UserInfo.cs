using SharpBoot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Dmeo.Apsnet.Model
{
    [ConfigProperty("UserInfo")]
    public class UserInfo
    {
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
