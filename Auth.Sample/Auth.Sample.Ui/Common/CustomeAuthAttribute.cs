using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Sample.Ui
{
    public class CustomeAuthAttribute : AuthorizeAttribute
    {

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //}

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var currentUser = SessionHelper.Get("Auth_User");
        //    if (currentUser == null) return false;
        //    return  base.AuthorizeCore(httpContext);
        //}
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { isSuccess = false, msg = "当前登录已经失效，请重新登录"  }
                };
                return;
            }
            base.HandleUnauthorizedRequest(filterContext);
        }

    }
}