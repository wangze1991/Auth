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
    public class HomeController : BaseController
    {

        private IModuleApplication _moduleApplication = null;
        private IWorkContext _workContext = null;

        public HomeController(IModuleApplication moduleApplication,IWorkContext context) {
            this._moduleApplication = moduleApplication;
            this._workContext = context;
        }

        // GET: Home
        public ActionResult Index()
        {
            var list = _moduleApplication.GetMenuByUserId(_workContext.CurrentUserId).OrderBy(x => x.Sort);
            return View(list);
        }
        public ActionResult Test() {
            return View();
        }
    }
}