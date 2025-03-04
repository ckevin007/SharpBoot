using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Common.Interfaces;
using SharpBoot.Common.Utils;
using SharpBoot.Common.Extenssions;
using SharpBoot.Models;
using SharpBoot.Services.Impls;
using SharpBoot.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharpBoot.Startups
{

    public class ComponentInjectStartup
    {
        public static ComponentInjectStartup Instance { get; } = new ComponentInjectStartup();

        private IServiceCollection services;
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
            SharpBootApplication.AssemblyList?.ForEach(a =>
            {
                InjectAssembly(a);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
        }

        private void InjectAssembly(Assembly assembly)
        {
            List<IStartupConfig> list = new List<IStartupConfig>();
            Type[] types = AttributeExtension.GetAttributeMarkTypes<ComponentAttribute>(assembly, true);
            types?.ToList().ForEach(t =>
            {
                InjectType(t);
            });
        }

        private void InjectType(Type t)
        {
            if (typeof(IStartupConfig).IsAssignableFrom(t))
            {
                InjectType(t, ComponentLifeTime.Singleton);
            }
            else
            {
                ComponentAttribute component = AttributeExtension.GetAttribute<ComponentAttribute>(t, true);
                InjectType(t, component.LifeTime);
            }

            ImportAttribute importAttribute = AttributeExtension.GetAttribute<ImportAttribute>(t, true);
            if (importAttribute != null && importAttribute.Value != null && importAttribute.Value.Length > 0)
            {
                foreach (var itm in importAttribute.Value)
                {
                    InjectType(itm, importAttribute.LifeTime);
                }
            }
        }

        private void InjectType(Type t, ComponentLifeTime lifeTime)
        {

            var methods = t.GetMethods().Where(a => a.GetCustomAttribute<BeanAttribute>() != null).ToList();
            if (methods != null && methods.Count > 0)
            {
                object getBean(IServiceProvider x, MethodInfo method)
                {
                    var param = method.GetParameters();
                    var paramObject = new object[param == null ? 0 : param.Length];
                    if (param != null)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            paramObject[i] = AutowiredUtils.InjectType(x, param[i].ParameterType, param[i].GetCustomAttribute<AutowiredAttribute>());
                        }
                    }
                    object rtn = method.Invoke(x.GetService(t), paramObject);
                    BeanAttribute beanAttribute = method.GetCustomAttribute<BeanAttribute>();
                    if (!string.IsNullOrEmpty(beanAttribute.Name))
                    {
                        var options = new ProxyGenerationOptions();
                        options.AddMixinInstance(new DependencyAddition() { BeanName = beanAttribute.Name });

                        ProxyGenerator proxyGenerator = new ProxyGenerator();

                        var instance = proxyGenerator.CreateClassProxyWithTarget(rtn.GetType(), rtn, options, new DefaultInterceptor());
                        BeanUtils.CopyObject(rtn, instance);
                        rtn = instance;
                    }
                    AutowiredUtils.AutoInject(ref rtn, x);
                    return rtn;
                }
                foreach (var method in methods)
                {
                    var attribute = method.GetCustomAttribute<BeanAttribute>();
                    Type returnType = method.ReturnType;
                    if (attribute.LifeTime == ComponentLifeTime.Scoped)
                    {
                        services.AddScoped(returnType, x => getBean(x, method));
                    }
                    if (attribute.LifeTime == ComponentLifeTime.Singleton)
                    {
                        services.AddSingleton(returnType, x => getBean(x, method));
                    }
                    if (attribute.LifeTime == ComponentLifeTime.Transient)
                    {
                        services.AddTransient(returnType, x => getBean(x, method));
                    }
                }
            }

            if (lifeTime == ComponentLifeTime.Singleton)
            {
                InjectTypeSingleton(t);
                return;
            }
            if (lifeTime == ComponentLifeTime.Transient)
            {
                InjectTypeTransient(t);
                return;
            }
            if (lifeTime == ComponentLifeTime.Scoped)
            {
                InjectTypeScoped(t);
                return;
            }
        }

        public object CreateNewInstance(Type t, IServiceProvider provider)
        {
            InterceptorAttribute[] attributes = t.GetCustomAttributes<InterceptorAttribute>().ToArray();
            Type[] interceptorTypes = attributes?.SelectMany(a => a.InterceptorTypes ?? (new Type[0])).ToArray();
            IInterceptor[] interceptors = interceptorTypes?.Select(i =>
             {
                 return (IInterceptor)provider.GetRequiredService(i);
             }).ToArray();

            var constructors = t.GetConstructors();
            var cons = constructors.OrderBy(a => a.GetParameters() == null ? 0 : a.GetParameters().Length).FirstOrDefault();
            object[] param = new object[cons.GetParameters() == null ? 0 : cons.GetParameters().Length];
            for (int i = 0; i < param.Length; i++)
            {
                Type paramType = cons.GetParameters()[i].ParameterType;
                //param[i] = provider.GetService(paramType);
                var attribute = cons.GetParameters()[i].GetCustomAttribute<AutowiredAttribute>();
                param[i] = AutowiredUtils.InjectType(provider,
                   paramType, attribute);
            }
            if (interceptors == null || interceptors.Length == 0)
            {
                var obj = Activator.CreateInstance(t, param);
                AutowiredUtils.AutoInject(ref obj, provider);
                AfterComponentBuilded(obj, provider);
                return obj;
            }

            ProxyGenerator proxyGenerator = new ProxyGenerator();
            var instance = proxyGenerator.CreateClassProxy(t, param, interceptors);
            AutowiredUtils.AutoInject(ref instance, provider);
            AfterComponentBuilded(instance, provider);
            return instance;
        }

        private void AfterComponentBuilded(object instance, IServiceProvider provider)
        {
            //bean被创建之后，执行被PostConstructAttribute标注的方法
            var methods = instance.GetType().GetMethods().Where(a => a.GetCustomAttribute<PostConstructAttribute>() != null).ToList();
            if (methods != null && methods.Count > 0)
            {
                methods = methods.OrderBy(a =>
                {
                    var pc = a.GetCustomAttribute<PostConstructAttribute>();
                    return pc.Order;
                }).ToList();
                foreach (var itm in methods)
                {
                    var itmParams = itm.GetParameters();
                    var paramObject = new object[itmParams == null ? 0 : itmParams.Length];
                    if (itmParams != null)
                    {
                        for (int i = 0; i < itmParams.Length; i++)
                        {
                            paramObject[i] = AutowiredUtils.InjectType(provider,
                                itmParams[i].ParameterType,
                                itmParams[i].GetCustomAttribute<AutowiredAttribute>());
                        }
                    }
                    object rtn = itm.Invoke(instance, paramObject);
                }
            }
        }

        private void InjectTypeSingleton(Type t)
        {
            services.AddSingleton(t, x => CreateNewInstance(t, x));
            var interfaces = t.GetInterfaces();
            interfaces?.ToList().ForEach(i =>
            {
                services.AddSingleton(i, x => x.GetService(t));
            });
        }




        private void InjectTypeScoped(Type t)
        {
            services.AddScoped(t, x => CreateNewInstance(t, x));
            var interfaces = t.GetInterfaces();
            interfaces?.ToList().ForEach(i =>
            {
                services.AddScoped(i, x => x.GetService(t));
            });
        }
        private void InjectTypeTransient(Type t)
        {
            services.AddTransient(t, x => CreateNewInstance(t, x));
            var interfaces = t.GetInterfaces();
            interfaces?.ToList().ForEach(i =>
            {
                services.AddTransient(i, x => x.GetService(t));
            });
        }



    }
}
