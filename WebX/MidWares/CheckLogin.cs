﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebX.MidWares
{
    public class CheckLogin : ActionFilterAttribute
    {
        public string Message { get; set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}
