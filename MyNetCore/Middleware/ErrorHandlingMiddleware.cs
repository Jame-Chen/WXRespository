using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using log4net;
using MyNetCore.Models;

namespace MyNetCore.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ErrorHandlingMiddleware));
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // if      (ex is NotFoundException)     code = HttpStatusCode.NotFound;
            // else if (ex is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (ex is MyException)             code = HttpStatusCode.BadRequest;
            code = HttpStatusCode.OK;
            var result = JsonConvert.SerializeObject(new Result { Code = "500", Msg = "程序错误", Data = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            log.Error(
                $"地址：{context.Request.Path} \n " +
                $"错误信息：{ex.Message} \n " +
                $"堆栈：{ex.StackTrace} \n " 
             );
            return context.Response.WriteAsync(result);
        }

    }
}
