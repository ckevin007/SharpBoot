using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Freesql.Extension
{
    public static class IInvocationExtension
    {
        public static void Execute(this IInvocation invocation)
        {
            invocation.Proceed();
            {
                var returnType = invocation.Method.ReturnType;
                if (returnType != null && returnType == typeof(Task)) //判断如果为Task类型
                {
                    Task task = (Task)invocation.ReturnValue;
                    task.Wait();    //设置返回值为刚刚定义的异步方法
                    return;
                }
            }

            {
                var returnType = invocation.Method.ReturnType;     //获取被代理方法的返回类型
                if (returnType != null && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    //HandleAsyncWithReflection(invocation);
                    Task task = (Task)invocation.ReturnValue;
                    task.Wait();
                    return;
                }
            }
        }

        //利用反射获取方法类型信息来构造等待返回值的异步方法
        private static void HandleAsyncWithReflection(IInvocation invocation)
        {
            var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            var mi = invocation.Method.MakeGenericMethod(resultType);
            invocation.ReturnValue = mi.Invoke(invocation, new[] { invocation.ReturnValue });
        }

    }
}
