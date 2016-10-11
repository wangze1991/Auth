using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auth.Sample.Domain;
using Auth.Sample.Application;
using Newtonsoft.Json;
using Utils;
namespace Auth.Sample.Ui.Controllers
{

    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : BaseController
    {
        private IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserList(EasyUiPage ePage) {
            return Json(this._userApplication.EasyPage(ePage), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser(int id)
        {
            return Json(new JsonDataConsequence(true,_userApplication.Get(id)),JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveUser(T_User user) {
            if (user.Id == 0)//现在
            {
                user.CreateTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;
                _userApplication.Insert(user);
                return Json(true, "保存成功");
            }
            else
            {
              var entity= _userApplication.Get(user.Id);
              if (entity == null) throw new Exception("当前修改的对象不存在");
              entity.UpdateTime = DateTime.Now;
              entity.UserName = user.UserName;
              entity.Remark = user.Remark;
              entity.IsOpen = user.IsOpen;
              entity.Sex = user.Sex;
              entity.Mobile = user.Mobile;
              entity.QQ = user.QQ;
              _userApplication.Update(user);
              return Json(true, "修改成功");
            }
        }

    }
}