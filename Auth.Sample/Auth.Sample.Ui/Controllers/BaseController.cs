using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using Utils;
using Auth.Sample.Infrastructure;
namespace Auth.Sample.Ui.Controllers
{

    [CustomeAuth]
    [CustomeException]
    [Compress]
    public class BaseController : Controller
    { 


        protected override JsonResult Json(object data, string contentType,
                  Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        protected JsonResult Json(object data, JsonRequestBehavior behavior, string dateTimeFormatting)
        {
            return new JsonNetResult()
            {
                Data = data,
                DateFormatt = dateTimeFormatting,
                JsonRequestBehavior = behavior
            };
        }

        protected virtual JsonResult Json(bool isSuccess, string msg, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { isSuccess, msg }, behavior);
        }
        protected virtual JsonResult Json(bool isSuccess, object data, string msg = null, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { isSuccess, data, msg }, behavior);
        }

    }

    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings SerializerSettings { get; set; }

        public string DateFormatt { get; set; }
        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if ((this.JsonRequestBehavior == System.Web.Mvc.JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Get请求不被允许");
            }
            if (DateFormatt.IsNullOrEmpty())
            {
                DateFormatt = DateTypeFormatt.yyyyMMddHHmmss;
            }
            var response = context.HttpContext.Response;
            response.ContentType =
                !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                var writer = new JsonTextWriter(response.Output)
                {
                    Formatting = Formatting.Indented,
                    DateFormatString = DateFormatt
                };
                var serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}