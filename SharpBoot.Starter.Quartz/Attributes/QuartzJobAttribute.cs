using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.Quartz.Attributes
{
    public class QuartzJobAttribute : Attribute
    {
        public string Cron { get; set; }
        public string Name { get; set; }

        public QuartzJobAttribute(string cron, string name = "")
        {
            this.Cron = cron;
            this.Name = name;
        }
    }
}
