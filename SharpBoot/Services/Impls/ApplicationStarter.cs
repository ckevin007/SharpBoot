using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Extenssions;

namespace SharpBoot.Services.Impls
{
    [Component]
    public class ApplicationStarter
    {
        [Autowired]
        private List<IApplicationRunner> runners;

        public void Run(string[] args = null)
        {
            runners?.ForEach(itm => itm.Run(args));
        }
    }
}
