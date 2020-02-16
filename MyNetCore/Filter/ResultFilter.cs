using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyNetCore.Models;

public class ResultFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        //根据实际需求进行具体实现
        if (context.Result is StatusCodeResult)
        {
            var objectResult = context.Result as StatusCodeResult;
            var msg = "";
            if (objectResult.StatusCode == 200)
            {
                msg = "操作成功!";
            }
            if (objectResult.StatusCode == 404)
            {
                msg = "未找到资源!";
            }
            context.Result = new ObjectResult(new Result { Code = objectResult.StatusCode.ToString(), Msg = msg, Data = "" });
        }
        else if (context.Result is EmptyResult)
        {
            context.Result = new ObjectResult(new Result { Code = "404", Msg = "未找到资源!", Data = "" });
        }
        else if (context.Result is ContentResult)
        {
            context.Result = new ObjectResult(new Result { Code = "200", Msg = "操作成功!", Data = (context.Result as ContentResult).Content });
        }
        else if (context.Result is ObjectResult)
        {
            var objectResult = context.Result as ObjectResult;
            var msg = "";
            if (objectResult.StatusCode == 200)
            {
                msg = "操作成功!";
            }
            if (objectResult.StatusCode == 404)
            {
                msg = "未找到资源!";
            }
            if (objectResult.StatusCode == 400)
            {
                msg = "数据验证失败!";
            }
            context.Result = new ObjectResult(new Result{ Code = objectResult.StatusCode.ToString(), Msg = msg, Data = objectResult.Value != null ? objectResult.Value : "" });

        }
    }
}