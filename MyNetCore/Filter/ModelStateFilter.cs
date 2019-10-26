using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string ret = "";
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        ret += error.ErrorMessage + "|";
                    }
                }
                var data = new
                {
                    code = 400,
                    msg = "数据验证失败!",
                    result = ret
                };
                context.Result = new JsonResult(data);
            }
        }
    }

