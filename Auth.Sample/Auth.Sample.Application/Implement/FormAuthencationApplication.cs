using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Auth.Sample.Domain;


namespace Auth.Sample.Application
{
    public class FormAuthencationApplication : IAuthenticationApplication
    {

       
        private readonly TimeSpan _expirationTimeSpan;

        private T_User _cachedUser;

        public FormAuthencationApplication()
        {
           // _userService = userService;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        public void SignIn(T_User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(1, user.UserName, now, now.Add(_expirationTimeSpan),
                createPersistentCookie, user.Id.ToString(), FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) ;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
           // cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            //HttpContext.Response.Cookies.Add(cookie);
            HttpContext.Current.Response.Cookies.Add(cookie);
            //nop源码中没有这一句，务必保证webconfig中的认证是form的。
            //FormsAuthentication.SetAuthCookie(user.UserName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
        //public HttpContextBase HttpContext
        //{
        //    get { return new HttpContextWrapper(System.Web.HttpContext.Current); }
        //}
    }
}
