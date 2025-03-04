using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Extenssions;
using SharpBoot.Common.Services;
using SharpBoot.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpBoot.Utils
{

    public static class ValueInjectUtils
    {
        public static void ValueConfiguration(ref object obj, IServiceProvider provider)
        {
            if (typeof(IValueInjecter).IsAssignableFrom(obj.GetType())) return;
            List<IValueInjecter> valueInjecters = provider.GetService<IEnumerable<IValueInjecter>>().ToList();
            if (valueInjecters == null || valueInjecters.Count == 0) return;
            var localDefaultValueInjecter = valueInjecters.FirstOrDefault(a => a.ValueAttributeType == typeof(ValueAttribute));
            InjectValue(ref obj, localDefaultValueInjecter);

            valueInjecters.Remove(localDefaultValueInjecter);
            foreach (var valueInject in valueInjecters)
            {
                InjectValue(ref obj, valueInject);
            }
        }

        private static void InjectValue(ref object obj, IValueInjecter valueInjecter)
        {

            ConfigPropertyInject(ref obj, valueInjecter);

            var pts = obj.GetType().GetProperties(AutowiredUtils.Binding);
            Type valueAttributeType = valueInjecter.ValueAttributeType;
            if (valueAttributeType == null) return;
            foreach (var property in pts)
            {
                var attribute = property.GetCustomAttribute(valueAttributeType);
                if (attribute == null) continue;
                if (attribute.GetType() != valueAttributeType) continue;
                var attr = attribute as ValueAttribute;
                property.SetValue(obj, valueInjecter.Get(property.PropertyType, attr.Name));
            }
            var fields = obj.GetType().GetFieldList();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute(valueAttributeType);
                if (attribute == null) continue;
                if (attribute.GetType() != valueAttributeType) continue;
                var attr = attribute as ValueAttribute;
                field.SetValue(obj, valueInjecter.Get(field.FieldType, attr.Name));
            }
        }

        private static void ConfigPropertyInject(ref object obj, IValueInjecter valueInjecter)
        {
            Type configPropertyType = valueInjecter.ConfigPropertyType;
            if (configPropertyType == null) return;
            Attribute targetConfigProperty = obj.GetType().GetCustomAttribute(configPropertyType);
            if (targetConfigProperty == null) return;
            if (targetConfigProperty.GetType() != configPropertyType) return;

            ConfigPropertyAttribute configProperty = (ConfigPropertyAttribute)targetConfigProperty;


            var newObj = valueInjecter.Get(obj.GetType(), configProperty.ConfigPath);


            if (newObj == null)
            {
                //throw new Exception($"无法注入 {obj.GetType().FullName} , 配置文件中找不到 {configProperty.ConfigPath} ");
            }
            else
            {
                obj = newObj;
            }
        }
    }
}
