using SharpBoot.Starter.WebApiClient.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Starter.WebApiClient.attribute
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class WebApiAttribute : Attribute
    {
        public WebApiAttribute(WebApiOption option)
        {
            Option = option;
        }

        public WebApiAttribute(string optionConfigName)
        {
            OptionConfigName = optionConfigName;
        }

        public WebApiOption Option { get; set; }

        public string OptionConfigName { get; set; }
    }
}
