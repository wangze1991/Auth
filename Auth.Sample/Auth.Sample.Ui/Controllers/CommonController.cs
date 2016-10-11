using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Utils;
namespace Auth.Sample.Ui.Controllers
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class CommonController : BaseController
    {
        // GET: Common
        public ActionResult Icon()
        {
            return View();
        }

        public ActionResult GetIcon(EasyUiPage pager)
        {
            pager.Rows = 50;//默认50分页
            var total = 0;
            string path = @"~/static/css/easyui/icons/";
            string iconFormat = "<li title=\"/static/css/easyui/icon/{0}.png\"><span class=\"icon icon-{0}\"></span></li>";
            var files = Directory.GetFiles(Server.MapPath(path));
            StringBuilder buffer = new StringBuilder();
            buffer.Append("<ul class=\"iconlist\">");
            files.Skip((pager.Page - 1) * pager.Rows).Take(pager.Rows).ForEach(x =>
            {
                buffer.Append(iconFormat.StringFormat(Path.GetFileNameWithoutExtension(x)));
            });
            total = files.Count() % pager.Rows == 0 ? files.Count() / pager.Rows : files.Count() / pager.Rows + 1;
            buffer.Append("</ul>");
            return Json(new { pages = total, data = buffer.ToString() },JsonRequestBehavior.AllowGet);
        }
    }
}