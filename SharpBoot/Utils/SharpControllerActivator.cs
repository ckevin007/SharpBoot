using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using SharpBoot.Startups;
using System;

namespace SharpBoot.Utils
{
    public class SharpControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));
            Type serviceType = actionContext.ActionDescriptor.ControllerTypeInfo.AsType();
            var provider = actionContext.HttpContext.RequestServices;
            //var target = Activator.CreateInstance(serviceType);
            var target = ComponentInjectStartup.Instance.CreateNewInstance(serviceType, provider);
            //AutowiredUtils.AutoInject(ref target, provider);
            return target;
        }

        public virtual void Release(ControllerContext context, object controller)
        {

        }
    }
}
