using LibrarySystem.Admin.Common;
using LibrarySystem.Core.Infrastructure;
using LibrarySystem.IService; 
using System;
using System.Web.Mvc;

namespace LibrarySystem.Admin.Infrastructure
{
    /// <summary>
    /// 账号验证
    /// </summary>
    public class AccountAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            int accountId = Loginer.AccountId;
            if (accountId == 0)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}