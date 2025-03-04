using Microsoft.AspNetCore.Http;
using SharpBoot.Auth.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Auth.extension
{
    public static class HttpContextExtentions
    {
        public static void SetUser<T>(this HttpContext context, T user) where T : IUserVo
        {
            context.Items["UserInfo"] = user;
        }
        public static T GetUser<T>(this HttpContext context) where T : IUserVo
        {
            object obj = context.Items["UserInfo"];
            if (obj == null) return default;
            return (T)obj;
        }

        public static string GetRequestId(this HttpContext context)
        {
            string requestId = context.Items["RequestId"]?.ToString();
            if (string.IsNullOrEmpty(requestId))
            {
                requestId = Guid.NewGuid().ToString("N")[0..9];
                context.Items["RequestId"] = requestId;
            }
            return requestId;
        }
    }
}
