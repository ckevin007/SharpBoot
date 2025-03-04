using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Services;
using SharpBoot.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SharpBoot.Demo.Controller_Service.impl
{
    [Component(ComponentLifeTime.Scoped)]
    public class CheckControllcerImpl : ICheckControllerService
    {

        [Autowired]
        public IList<IUserService> UserServices { get; set; }

        public object Get()
        {
            return UserServices.ToList()?[0].Get();
        }
    }
}
