﻿using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MyNetCore.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        private string LogFlag { get; set; }
        private string ActionArguments { get; set; }
        private static ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ActionFilter));

        /// <summary>
        /// 请求体中的所有值
        /// </summary>
        private string RequestBody { get; set; }

        private Stopwatch Stopwatch { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // 后续添加了获取请求的请求体，如果在实际项目中不需要删除即可
            long contentLen = context.HttpContext.Request.ContentLength == null ? 0 : context.HttpContext.Request.ContentLength.Value;
            if (contentLen > 0)
            {
                // 读取请求体中所有内容
                System.IO.Stream stream = context.HttpContext.Request.Body;
                if (context.HttpContext.Request.Method == "POST")
                {
                    stream.Position = 0;
                }
                byte[] buffer = new byte[contentLen];
                stream.Read(buffer, 0, buffer.Length);
                // 转化为字符串
                RequestBody = System.Text.Encoding.UTF8.GetString(buffer);
            }

            ActionArguments = Newtonsoft.Json.JsonConvert.SerializeObject(context.ActionArguments);

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Stopwatch.Stop();
            string url = context.HttpContext.Request.Host + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            string method = context.HttpContext.Request.Method;
            string qs = ActionArguments;
            string res = "在返回结果前发生了异常";
            try
            {
                dynamic result = context.Result == null ? new { Value = "无返回结果" } : context.Result as dynamic;
                if (result != null)
                {
                    res = Newtonsoft.Json.JsonConvert.SerializeObject(result.Value);
                }
            }
            catch (System.Exception e)
            {
                res = e.Message;
            }
            log.Info(//$"\n 方法：{LogFlag} \n " +
                $"地址：{url} \n " +
                $"方式：{method} \n " +
                $"请求体：{RequestBody} \n " +
                $"参数：{qs}\n " +
                $"结果：{res}\n " +
                $"耗时：{Stopwatch.Elapsed.TotalMilliseconds} 毫秒（指控制器内对应方法执行完毕的时间）");

        }
    }
}
