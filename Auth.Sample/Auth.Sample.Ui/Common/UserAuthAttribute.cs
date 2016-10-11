using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Auth.Sample.Ui
{
    
    public class UserAuthAttribute:ActionFilterAttribute,IActionFilter
    {





        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Write(filterContext.RouteData.Values["controller"] );
            HttpContext.Current.Response.Write(filterContext.RouteData.Values["action"]);
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))//允许匿名登录
            {
                base.OnActionExecuting(filterContext);
                return;
            }
             //没有设置权限，直接通过




            //设置权限，验证


        }

    }
}