using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Extensions;
using PBIPortal.Core.ObjectActionValidator;
using PBIPortal.Core.Services;
using PBIPortal.Core.Utilities;

namespace PBIPortal.Core.Filters
{
    public class ActionExecuteFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //验证方法参数
            context.ActionParamsValidator();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}