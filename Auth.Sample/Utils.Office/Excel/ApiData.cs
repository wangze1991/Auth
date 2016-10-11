using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utils.Office
{
    public class ApiData:IApiData
    {
        public object GetData(HttpContext context) 
        {
            try
            {
                var pageParam =new {page=1,rows=int.MaxValue };
                var param = JsonConvert.DeserializeAnonymousType(context.Request["dataParams"],pageParam);
                var titles = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<Column>>>(context.Request["titles"]);
                var url = context.Request["action"];
                var result = JsonConvert.DeserializeObject<dynamic>(HttpHelper.GetGetString(url));
                if (result.rows != null)
                {
                    return result.rows;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IDictionary<string, string> GetDataParams(dynamic p)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
    
            Type type = (p as object).GetType();
            foreach (var item in type.GetProperties(BindingFlags.Public))
            {
                dic.Add(item.Name,item.GetValue(p));
            }
            return dic;
        }
    }
}
