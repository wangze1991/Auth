using Auth.Sample.Application;
using Auth.Sample.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utils;

namespace Auth.Sample.Ui.Controllers
{
    public class ButtonController : BaseController
    {
        private IButtonApplication _application;

        private IWorkContext _workContext;

        public ButtonController(IButtonApplication application,IWorkContext context)
        {
            this._application = application;
            _workContext = context;
        }

        // GET: Button
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetButtonList(EasyUiPage ePage)
        {
            return Json(this._application.EasyPage(ePage), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(IEnumerable<T_Button> buttons)
        {
            this._application.SaveOrUpdate(buttons);
            return Json(true, "保存成功");
        }

        public ActionResult ChooseButton()
        {
            return View(this._application.LoadAll());
        }

        public ActionResult GetButtonsByModuleId(string moduleId)
        {
            var data = this._application.LoadButtonsByModuleId(moduleId).Select(x => x.Id);
            return Json(true, data);
        }

        public ActionResult Save(string moduleId, int[] buttonIds)
        {
            this._application.AddButtonModuleRelationship(moduleId, buttonIds);
            return Json(true, "保存成功");
        }

        public ActionResult GetButtonsByUser() {

            return View();
        }
    }
}