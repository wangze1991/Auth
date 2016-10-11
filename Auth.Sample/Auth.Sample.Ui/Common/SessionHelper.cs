using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;
namespace Auth.Sample.Ui
{
    public class SessionHelper
    {  
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="value">Session值</param>
        public static void Add(string strSessionName, object value)
        {
            HttpContext.Current.Session[strSessionName] = value;
            //HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName];
            }
        }
        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }

    }
}