using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Common.Model
{
    public class StaticFileConfig
    {
        public bool Enable { get; set; }
        public string LocalPath { get; set; }

        public string UrlPath { get; set; }
    }
}
