using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using MyNetCore.Models;

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
                var data = new Result
                {
                    Code = "400",
                    Msg = "数据验证失败!",
                    Data = ret
                };
                context.Result = new JsonResult(data);
            }
        }
    }

