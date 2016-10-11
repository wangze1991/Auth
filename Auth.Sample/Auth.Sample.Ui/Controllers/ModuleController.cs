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
    /// 模块 菜单表
    /// </summary>
    public class ModuleController : BaseController
    {
        private IModuleApplication _moduleApplication;
        private IRoleModuleButtonApplication _roleModuleButtonApl;
        private IWorkContext _workContext;

        public ModuleController(IModuleApplication moduleApplication,IRoleModuleButtonApplication roleModuleButtonApl,IWorkContext workContext)
        {
            this._moduleApplication = moduleApplication;
            this._roleModuleButtonApl = roleModuleButtonApl;
            this._workContext = workContext;
        }

        // GET: Module
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetModule()
        {
            var list = _moduleApplication.GetMenuByUserId(_workContext.CurrentUserId).OrderBy(x=>x.Sort);
            return Json(new {total=list.Count(),rows=list }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public ActionResult AddOrEditModule(string modules)
        {
            _moduleApplication.SaveOrUpdate(modules);
            return Json(true, "保存成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditModule()
        {
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteModule()
        {
            return View();
        }
        public ActionResult RoleModuleButton() {
            return View();
        }
        public ActionResult GetModuleWithButtons() {
            var list = _moduleApplication.LoadAllWithButtons();
            return Json(new{total=list.Count(),rows=list});
        }
        /// <summary>
        /// 保存关系
        /// </summary>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public ActionResult SaveRoleModuleButton([Bind(Exclude="Id")]T_Role_Module_Button relationship)
        {
           var model= _roleModuleButtonApl.Get(relationship);
           if (model == null)
           {
               _roleModuleButtonApl.Insert(relationship);
           }
           else
           {
               _roleModuleButtonApl.Delete(relationship);
           }

            return Json(true,"保存成功",JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// 根据角色id 获取权限列表。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult GetRoleModuleButton(int roleId)
        {
           return Json(true,_roleModuleButtonApl.LoadByRoleId(roleId),"");
        }



    }
}