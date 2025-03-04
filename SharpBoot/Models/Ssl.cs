using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Models
{
    public class Ssl
    {
        public bool Enable { get; set; }
        public string PfxPath { get; set; }
        public string KeyPath { get; set; }
        public string Key { get; set; }
    }
}
