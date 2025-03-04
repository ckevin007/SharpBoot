using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Services
{
    public interface IDependencyAddition
    {
        string BeanName { get; set; }
    }
}
