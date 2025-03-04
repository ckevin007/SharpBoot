using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using SharpBoot.QuickApi.exceptions;
using SharpBoot.QuickApi.model;

namespace SharpBoot.QuickApi.filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is MsgException))
            {
                context.Result = new ObjectResult(Result.Fail(context.Exception.Message));
                return;
            }
            MsgException msg = (MsgException)context.Exception;
            context.Result = new ObjectResult(new Result(msg.ResultCode, msg.ResultCode.Msg, msg.Data));
        }
    }
}
