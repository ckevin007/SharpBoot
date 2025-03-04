using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SharpBoot.Starter.Freesql.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;
        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }


        private IUnitOfWork _uow;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var uowm = context.HttpContext.RequestServices.GetService<UnitOfWorkManager>();
            _uow = uowm.Begin(this.Propagation, this.IsolationLevel);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var ex = context.Exception;
            try
            {
                if (ex == null) _uow.Commit();
                else _uow.Rollback();
            }
            finally
            {
                _uow.Dispose();
            }
        }
    }
}
