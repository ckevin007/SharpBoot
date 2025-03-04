using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Common.Interfaces
{
    public interface IApplicationRunner
    {
        void Run(string[] args = null);
    }
}
