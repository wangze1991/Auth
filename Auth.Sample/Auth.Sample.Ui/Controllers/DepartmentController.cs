using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auth.Sample.Domain;
using Auth.Sample.Application;
using Newtonsoft.Json;
using Utils;
using Auth.Sample.Domain.Dto;
namespace Auth.Sample.Ui.Controllers
{
    public class DepartmentController : BaseController
    {

        private readonly IDepartmentApplication _application;

        public DepartmentController(IDepartmentApplication application)
        {
            this._application = application;
        }


        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetDeparment(int id = 0)
        //{
           
        //}

        public ActionResult GetChild(string id="0")
        {
            return Json(this._application.GetChild(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(T_Department department) {
            _application.Insert(department);
            return Json(true, "保存成功");
        
        }
        [HttpPost]
        public ActionResult Edit(DepartmentDto department) 
        {
            _application.Update(department);
            return Json(true,"修改成功");
        }

        public ActionResult Get(int id = 0)
        {
            return Json(_application.Get(id),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            _application.Delete(id);
            return Json(true, "删除成功");
        }
    }
}