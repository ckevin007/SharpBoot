using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SharpBoot.Auth.service;
using SharpBoot.Auth.extension;
using SharpBoot.Auth.model;
using Microsoft.Extensions.Configuration;
using Castle.Core.Internal;

namespace SharpBoot.Auth.filters
{
    public class AuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string[] permissions;

        public AuthAttribute() { }

        public bool AllowAnonymous { get; set; } = false;

        public AuthAttribute(params string[] permissions)
        {
            this.permissions = permissions;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionName = descriptor.ActionName;
                var ctrlName = descriptor.ControllerName;
                var methodInfo = descriptor.MethodInfo;
                var allowAnonymous = methodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
                if (allowAnonymous != null) return;
            }
            var configer = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var tokenHeaderKey = configer.GetSection("SharpBoot:Auth:TokenKey").Value;
            var enableAuth = configer.GetSection("SharpBoot:Auth:Enable").Get<bool>();
            if (!enableAuth) return;
            if (string.IsNullOrEmpty(tokenHeaderKey)) tokenHeaderKey = "token";
            string token = context.HttpContext.Request.Headers[tokenHeaderKey];
            if (string.IsNullOrEmpty(token))
            {
                context.HttpContext.Request.Query.TryGetValue(tokenHeaderKey, out StringValues token1);
                token = token1;
            }
            IAuthConfig authConfig = context.HttpContext.RequestServices.GetRequiredService<IAuthConfig>();
            if (string.IsNullOrEmpty(token) && !AllowAnonymous)
            {
                Response(context, authConfig.NotLoginResult);
                return;
            }
            var tokenService = authConfig.GetTokenService(context.HttpContext.RequestServices);
            var user = await tokenService.GetUserByToken(token);

            if (!AllowAnonymous)
            {
                if (user == null)
                {
                    Response(context, authConfig.NotLoginResult);
                    return;
                }
                if (!CheckPermissions(permissions, user.Permissions?.ToArray()))
                {
                    Response(context, authConfig.PowerErrorResult);
                    return;
                }
            }

            if (user != null)
            {
                context.HttpContext.SetUser(user);
            }

        }


        private bool CheckPermissions(string[] requires, string[] has)
        {
            if (requires == null || requires.Length == 0) return true;
            if (has == null || has.Length == 0) return false;
            foreach (var require in requires)
            {
                if (has.Any(a => CheckPermission(require, a)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckPermission(string require, string has)
        {
            if (string.IsNullOrEmpty(require) || string.IsNullOrEmpty(has)) return false;
            has = has + ":*";
            string[] rh = require.Split(':');
            string[] hh = has.Split(':');
            for (int i = 0; i < rh.Length; i++)
            {
                if (hh.Length <= i) return false;
                string rItm = rh[i];
                string hItm = hh[i];
                // if (rItm == "*") return true;
                if (hItm == "*" || rItm == "*") return true;
                if (hItm.ToLower() != rItm.ToLower()) return false;
            }
            return true;
        }

        private void Response(AuthorizationFilterContext context, object result)
        {
            context.Result = new ObjectResult(result);
        }
    }
}
