using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auth.Sample.Ui.Models;
using Auth.Sample.Domain;
using Auth.Sample.Application;
using Auth.Sample.Infrastructure;
using Utils;
namespace Auth.Sample.Ui.Controllers
{
    public class AccountController : BaseController
    {
        private IAccountApplicaton _accountApplication;
        private IAuthenticationApplication _authApplication;
         
        public AccountController(IAccountApplicaton accountApplication,IAuthenticationApplication authApplication)
        {
            this._accountApplication = accountApplication;
            this._authApplication = authApplication;
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LogOnModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!model.UserName.IsNullOrEmpty())
                {
                    model.UserName = model.UserName.Trim();
                }
                var loginResult = _accountApplication.ValidateUser(model.UserName, model.Password);
                if (loginResult.Item1 == UserLoginResult.登录成功)
                {
                    _authApplication.SignIn(loginResult.Item2, model.RememberMe);
                    if (returnUrl.IsNullOrEmpty())
                    {
                        //return RedirectToAction("Index", "Home");
                        return Json(new {isSuccess=true,url="/Home/Index" });
                    }
                    return Json(new { isSuccess = true, url = returnUrl });
                }
                return Json(new { isSuccess = false, msg = loginResult.Item1.ToString() });
            }
            return Json(new { isSuccess = false, msg ="输入错误,请重新输入" });
        }



        /// <summary>
        /// 注销登录
        /// </summary>
        public void SignOut() {
            _authApplication.SignOut();
        
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterUser() {
            return View();
        }
    }
}