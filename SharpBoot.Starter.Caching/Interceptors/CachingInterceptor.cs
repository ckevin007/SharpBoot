using Castle.DynamicProxy;
using Newtonsoft.Json;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Common.Interfaces;
using SharpBoot.Starter.Caching.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Caching.Interceptors
{
    [Component(LifeTime = ComponentLifeTime.Transient)]
    public class CachingInterceptor : IInterceptor
    {
        [Autowired]
        ICache cache;

        public void Intercept(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            CacheableAttribute cacheable = method.GetCustomAttribute<CacheableAttribute>();
            CacheputAttribute cacheput = method.GetCustomAttribute<CacheputAttribute>();
            CachedelAttribute cachedel = method.GetCustomAttribute<CachedelAttribute>();
            if (cacheable != null)
            {
                Cacheable(invocation, cacheable);
                return;
            }
            if (cacheput != null)
            {
                Cacheput(invocation, cacheput);
                return;
            }
            if (cachedel != null)
            {
                Cachedel(invocation, cachedel);
                return;
            }
            invocation.Proceed();
        }

        private void Cacheable(IInvocation invocation, CacheableAttribute attribute)
        {
            string key = GetKeys(attribute.KeyName, invocation, attribute.KeyParam)?.FirstOrDefault();
            if (string.IsNullOrEmpty(key)) throw new Exception("key不可为空");
            if (cache.Exists(key))
            {
                invocation.ReturnValue = cache.GetCache(key, invocation.Method.ReturnType);
                return;
            }
            else
            {
                invocation.Proceed();
                object value = invocation.ReturnValue;
                if (value == null) return;
                //if (value is Task task)
                //{
                //    var type = task.GetType();
                //    var property = type.GetProperty("Result");
                //    value = property.GetValue(task);
                //}
                cache.SetCache(key, value, attribute.ExpSecond);
            }
        }
        private void Cacheput(IInvocation invocation, CacheputAttribute attribute)
        {

            List<string> keys = GetKeys(attribute.KeyName, invocation, attribute.KeyParams);
            if (keys == null || keys.Count == 0) throw new Exception("key列表不可为空");
            invocation.Proceed();
            object value = invocation.ReturnValue;
            foreach (var key in keys)
            {
                cache.SetCache(key, value, attribute.ExpSecond);
            }
        }
        private void Cachedel(IInvocation invocation, CachedelAttribute attribute)
        {
            List<string> keys = GetKeys(attribute.KeyName, invocation, attribute.KeyParams);
            if (keys == null || keys.Count == 0) throw new Exception("key列表不可为空");
            invocation.Proceed();
            foreach (var key in keys)
            {
                cache.RemoveCache(key);
            }
        }

        private List<string> GetKeys(string keyName, IInvocation invocation, params string[] keyParam)
        {
            if (keyParam == null || keyParam.Length == 0) return new List<string> { keyName };

            return keyParam.Select(itm =>
            {
                List<string> param = ExtractMessageByRegular(itm);
                if (param == null || param.Count == 0) return keyName;
                string key = itm;
                param.ForEach(attr =>
                {
                    string attrValue = GetAttrValue(invocation, attr);
                    key = key.Replace($"{{{attr}}}", attrValue);
                });
                return keyName + key;
            }).ToList();
        }

        private string GetAttrValue(IInvocation invocation, string attr)
        {
            ParameterInfo[] parameterInfos = invocation.Method.GetParameters();
            object[] args = invocation.Arguments;
            if (parameterInfos == null || parameterInfos.Length == 0) return "";
            string attrName = attr.Split(".")[0];
            string attrProperty = attrName;
            if (attr.Contains("."))
            {
                List<string> sh = attr.Split(".").ToList();
                sh.RemoveAt(0);
                attrProperty = string.Join(".", sh);
            }
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                if (attrName == parameterInfos[i].Name)
                {
                    return GetPropertyValue(args[i], attrName, attrProperty);
                }
            }
            return "";
        }

        private string GetPropertyValue(object obj, string parentName, string propertyName)
        {
            if (parentName == propertyName)
            {
                return obj.ToString();
            }
            else
            {
                string[] sh = propertyName.Split(".");
                foreach (var itm in sh)
                {
                    Type type = obj.GetType();
                    PropertyInfo property = type.GetProperty(itm);
                    if (property == null) break;
                    obj = property.GetValue(obj);
                    if (obj == null) break;
                }
                return obj.ToString();
            }
        }

        private List<string> ExtractMessageByRegular(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return null;
            List<string> list = new List<string>();

            Regex p = new Regex("\\{([^}]*)\\}");
            Match m = p.Match(msg);
            for (int i = 0; i < m.Groups.Count; i++)
            {
                string str = m.Groups[i].Value;
                str = str.Substring(1, str.Length - 2);
                if (!string.IsNullOrEmpty(str))
                {
                    list.Add(str);
                }
            }
            return list;
        }
    }
}
