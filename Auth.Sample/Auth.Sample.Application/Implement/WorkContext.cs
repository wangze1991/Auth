using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Auth.Sample.Application
{
    public class WorkContext : IWorkContext
    {

        private IUserApplication _userApplication = null;

        public WorkContext(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }


        public Domain.T_User CurrentUser
        {
            get
            {
                if (HttpContext.Current == null) throw new ArgumentNullException();
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    FormsIdentity formIdentity = HttpContext.Current.User as FormsIdentity;
                    var id = formIdentity.Ticket.UserData;
                    var currentUser = _userApplication.Get(id);
                    if (currentUser == null) throw new Exception("用户未找到");
                    return currentUser;
                }
                throw new Exception("当前用户未登录");
            }
        }

        public int CurrentUserId
        {
            get
            {
                if (HttpContext.Current == null) throw new ArgumentNullException();
                if (HttpContext.Current.Request.IsAuthenticated && HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    FormsIdentity formIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                    return int.Parse(formIdentity.Ticket.UserData);

                }
                throw new Exception("当前用户未登录");
            }

        }
    }
}
