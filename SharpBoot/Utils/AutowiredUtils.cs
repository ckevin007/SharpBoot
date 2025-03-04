using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Extenssions;
using SharpBoot.Models;
using SharpBoot.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
namespace SharpBoot.Utils
{
    public static class AutowiredUtils
    {
        public static BindingFlags Binding =
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Default |
                BindingFlags.NonPublic;

        public static void AutoInject(ref object obj, IServiceProvider provider)
        {
            if (obj == null) return;
            AutoInjectField(obj, provider);
            var pts = obj.GetType().GetProperties(Binding);
            foreach (var property in pts)
            {
                var autowired = property.GetCustomAttribute<AutowiredAttribute>();
                if (autowired == null) continue;
                var propertyType = property.PropertyType;
                var target = InjectType(provider, propertyType, autowired);
                property.SetValue(obj, target);
            }
            ValueInjectUtils.ValueConfiguration(ref obj, provider);
        }

        private static List<object> InjectList(Type argType, IServiceProvider provider)
        {
            Type ieType = typeof(IEnumerable<>);
            ieType = ieType.MakeGenericType(argType);
            var servicesie = provider.GetService(ieType);

            List<object> rt = new List<object>();
            BindingFlags binding = BindingFlags.Public | BindingFlags.Instance;
            var enumerator = ieType.GetMethod("GetEnumerator", binding).Invoke(servicesie, null);
            MethodInfo moveInfoMethod = enumerator.GetType().GetMethod("MoveNext", binding);
            while ((bool)moveInfoMethod.Invoke(enumerator, null))
            {
                var obj = enumerator.GetType().GetProperty("Current").GetValue(enumerator);
                rt.Add(obj);
            }
            return rt;
        }

        private static object ChangeListType(List<object> list, Type desArgType)
        {
            if (list == null || list.Count == 0) return null;
            Type listType = typeof(List<>);
            listType = listType.MakeGenericType(desArgType);
            var rt = Activator.CreateInstance(listType);
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            MethodInfo methodInfo = listType.GetMethod("Add", flag);
            list.ForEach(a =>
            {
                methodInfo.Invoke(rt, new object[] { a });
            });
            return rt;
        }



        private static void AutoInjectField(object obj, IServiceProvider provider)
        {
            if (obj == null) return;
            var pts = obj.GetType().GetFieldList();
            foreach (var field in pts)
            {
                var autowired = field.GetCustomAttribute<AutowiredAttribute>();
                if (autowired == null) continue;
                var fieldType = field.FieldType;
                //if (obj.GetType().FullName == "SharpBoot.Demo.Services.Runner.TestRunner4")
                //{

                //}
                var target = InjectType(provider, fieldType, autowired);
                field.SetValue(obj, target);
            }
        }


        public static object InjectType(IServiceProvider provider, Type type, AutowiredAttribute autowired)
        {
            bool isList = typeof(IList).IsAssignableFrom(type);
            if (isList)
            {
                Type argType = type.GetGenericArguments()[0];
                var list = InjectList(argType, provider);
                list = Order(list, autowired);
                var rt = ChangeListType(list, argType);
                return rt;
            }
            else
            {
                var target = InjectFirst(provider, type, autowired);
                return target;
            }
        }

        private static object InjectFirst(IServiceProvider service, Type type, AutowiredAttribute autowired)
        {
            return InjectFirst(service.GetServices(type).ToList(), autowired);
        }

        private static object InjectFirst(List<object> services, AutowiredAttribute autowired)
        {
            if (services == null || services.Count == 0) return null;
            services = Order(services, autowired);
            if (services == null || services.Count == 0) return null;
            return services[0];
        }

        public static List<object> Order(List<object> services, AutowiredAttribute autowired)
        {
            if (services == null || services.Count == 0) return services;
            if (autowired != null && !string.IsNullOrEmpty(autowired.Name))
            {
                services = services.Where(itm =>
                {
                    if (itm == null) return false;
                    ComponentAttribute attribute = itm.GetType().GetCustomAttribute<ComponentAttribute>();
                    string attrName = "";
                    if (attribute != null)
                    {
                        attrName = attribute.Name;
                    }
                    else
                    {
                        if (ProxyUtil.IsProxy(itm))
                        {
                            var addition = itm as IDependencyAddition;
                            attrName = addition?.BeanName;
                        }
                    }
                    return attrName == autowired.Name;
                }).ToList();
            }
            if (services == null || services.Count == 0) return services;
            services = services.OrderBy(itm =>
            {
                ComponentAttribute attribute = itm.GetType().GetCustomAttribute<ComponentAttribute>();
                if (attribute == null) return 1;
                return attribute.Primary ? 0 : 1;
            }).ThenBy(itm =>
            {
                ComponentAttribute attribute = itm.GetType().GetCustomAttribute<ComponentAttribute>();
                if (attribute == null) return int.MaxValue;
                return OrderAttributeExtension.Order(itm.GetType());
            }).ToList();
            return services;
        }
    }
}
