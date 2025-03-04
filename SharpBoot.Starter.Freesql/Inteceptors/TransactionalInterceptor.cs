using Castle.DynamicProxy;
using FreeSql;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Starter.Freesql.Attributes;
using SharpBoot.Starter.Freesql.Extension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Starter.Freesql.Inteceptors
{
    [Component(LifeTime = ComponentLifeTime.Scoped)]
    public class TransactionalInterceptor : IInterceptor
    {
        [Autowired]
        private UnitOfWorkManager uowm;

        public void Intercept(IInvocation invocation)
        {
            MethodInfo method = invocation.TargetType.GetMethod(invocation.Method.Name);
            TransactionalAttribute transcational = method.GetCustomAttribute<TransactionalAttribute>();
            if (transcational == null)
            {
                invocation.Proceed();
                return;
            }
            var uow = uowm.Begin(transcational.Propagation, transcational.IsolationLevel);
            try
            {
                invocation.Execute();
                uow.Commit();
            }
            catch
            {
                uow.Rollback();
            }
            finally
            {
                uow.Dispose();
            }
        }
    }
}
