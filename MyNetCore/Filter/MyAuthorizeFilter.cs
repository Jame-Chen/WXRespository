using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using MyNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.Filter
{
    public class MyAuthorizeFilter : AuthorizeFilter
    {
        private static AuthorizationPolicy _policy_ = new AuthorizationPolicy(new[] {
            new DenyAnonymousAuthorizationRequirement() },
            new string[] { });

        public MyAuthorizeFilter() : base(_policy_) { }

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await base.OnAuthorizationAsync(context);
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var data = new Result
                {
                    Code = "401",
                    Msg = "身份验证失败!",
                    Data = null
                };
                context.Result = new JsonResult(data);
                return;
            }

        }
    }
}
