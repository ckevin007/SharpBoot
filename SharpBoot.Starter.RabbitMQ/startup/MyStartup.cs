
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Helper.Consumers;
using RabbitMQ.Helper.Injection.Attributes;
using RabbitMQ.Helper.Injection.Extensions;
using RabbitMQ.Helper.Injection.Helpers;
using RabbitMQ.Helper.Injection.Interfaces;
using RabbitMQ.Helper.Models;
using RabbitMQ.Helper.Providers;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.RabbitMQ.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpBoot.Starter.RabbitMQ.startup
{
    [Component]
    public class MyStartup : IStartupConfig
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMQHelper(x =>
            {
                x.AddProvider(p =>
                {
                    var rabbitConnectionFactorys = p.GetServices<IConnectionInfoFactory>().ToList();
                    ConnectionInfo connection = null;
                    foreach (var factory in rabbitConnectionFactorys)
                    {
                        connection = factory.GetConnectionInfo();
                        if (connection != null) break;
                    }
                    var provider = new RabbitMQProvider(connection);
                    return provider;
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.AddRabbitMQProductors(factory =>
            {

            });
            UseRabbitSubcribes(app);
        }


        public void UseRabbitSubcribes(IApplicationBuilder app)
        {
            List<IConsumer> consumers = app.ApplicationServices.GetServices<IConsumer>().ToList();
            if (consumers == null || consumers.Count == 0) return;
            var provider = app.ApplicationServices.GetService<RabbitMQProvider>();
            consumers.ForEach(a => BuildSubscribe(a, provider));
        }


        private void BuildSubscribe(object obj, RabbitMQProvider provider)
        {
            Type t = obj.GetType();
            var methods = t.GetMethods();
            if (methods == null || methods.Length == 0) return;

            foreach (var method in methods)
            {
                var subscribe = method.GetCustomAttribute<RabbitSubscribeAttribute>();
                if (subscribe == null) continue;
                ConsumerOptions options = new ConsumerOptions()
                {
                    SubscribeExchange = subscribe.Exchange,
                    SubscribeRouting = subscribe.Routing,
                    SubscribeQueue = subscribe.Queue,
                    Exclusive = subscribe.Exclusive,
                    Durable = subscribe.Durable,
                    AutoDelete = subscribe.AutoDelete,
                    Args = subscribe.Args,
                    PerfetchCount = subscribe.PerfetchCount,
                    AutoAck = subscribe.AutoAck
                };
                EventConsumer consumer = new EventConsumer(provider, options);
                bool isValueType = false;
                var paramArray = method.GetParameters();
                if (paramArray == null || paramArray.Length == 0) continue;
                var param = paramArray[0];
                var paramType = param.ParameterType;
                isValueType = paramType.IsValueType || paramType == typeof(string);
                bool returnBool = method.ReturnType == typeof(bool);
                consumer.Subscribe(msg =>
                {
                    try
                    {
                        object value = null;
                        if (isValueType)
                        {
                            value = Convert.ChangeType(msg, paramType);
                        }
                        else
                        {
                            value = JsonConvert.DeserializeObject(msg, paramType);
                        }
                        if (returnBool) return (bool)method.Invoke(obj, new object[] { value });
                        method.Invoke(obj, new object[] { value });
                        return true;
                    }
                    catch (Exception)
                    {

                    }
                    return false;
                });
            }
        }
    }
}
