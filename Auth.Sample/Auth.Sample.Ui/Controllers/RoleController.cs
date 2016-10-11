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
using Auth.Sample.Domain.Dto;
namespace Auth.Sample.Ui.Controllers
{
    public class RoleController : BaseController
    {
          private IRoleApplication _application;
          private IRoleUserApplication _userRoleApplication;

          public RoleController(IRoleApplication applicaton,IRoleUserApplication userRoleApplication)
          {
              this._application = applicaton;
              this._userRoleApplication = userRoleApplication;
          }


        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRoleList(EasyUiPage ePage)
        {
            var list = _application.Page(ePage,null,"Sort asc");
            return Json(new { total = list.total, rows = list.rows }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(RoleDto dto)
        {
            _application.SaveOrUpdate(dto);
            return Json(true, "保存成功");
        }

        public ActionResult Get(int id)
        {
            return Json(true, this._application.Get(id), "", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRolesByUserId(int userId) {
            var list = this._userRoleApplication.GetRolesByUserId(userId);
            var data = new JsonDataConsequence(true);
            if (list != null)
            {
                data.data = list.Select(x => x.Id).ToArray();
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加或者删除用户和角色之间的关系
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveRoleUser(int userId,int[] roles)
        {
            var result = _userRoleApplication.Save(userId, roles);
            return Json(new JsonConsequence(result),JsonRequestBehavior.AllowGet);
        }
    }

    public class Test
    {

        public int UserId { get; set; }

        public int[] Roles { get; set; }
    }
}