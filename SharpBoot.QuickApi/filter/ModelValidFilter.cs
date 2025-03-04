using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpBoot.QuickApi.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.filter
{
    public class ModelValidFilter : IAlwaysRunResultFilter
    {

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                string msg = "";
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        msg += error.ErrorMessage + ";";
                    }
                }
                context.Result = new ObjectResult(Result.Fail(msg));
            }
        }
    }
}
