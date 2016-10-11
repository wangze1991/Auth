using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Web;

namespace Utils
{
    public class HttpHelper
    {


        #region 同步
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary
        public static HttpWebResponse CreateGetHttpResponse(string url)
        {
            return HttpHelper.CreateGetHttpResponse(url, 0, null, null);
        }
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest request = null;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "GET";

                //设置代理UserAgent和超时
                if (!userAgent.IsNullOrEmpty()) request.UserAgent = userAgent;
                if (timeout != 0) request.Timeout = timeout;
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                return request.GetResponse() as HttpWebResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static HttpWebResponse CreatePostHttpResponse(string url)
        {
            return HttpHelper.CreatePostHttpResponse(url, null, 0, null, null);
        }
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters)
        {
            return HttpHelper.CreatePostHttpResponse(url, parameters, 0, null, null);
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int timeout, string userAgent, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest request = null;
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    //request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //设置代理UserAgent和超时
                //request.UserAgent = userAgent;
                //request.Timeout = timeout; 

                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                //发送POST数据  
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    foreach (string key in parameters.Keys)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    byte[] data = Encoding.ASCII.GetBytes(buffer.ToString().TrimStart('&'));
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                string[] values = request.Headers.GetValues("Content-Type");
                return request.GetResponse() as HttpWebResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取get的string
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetGetString(string url)
        {
            return HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(url));
        }
        /// <summary>
        /// 获取post的string
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPostString(string url)
        {
            return HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(url));
        }
        /// <summary>
        /// 获取post的string
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetPostString(string url, IDictionary<string, string> parameters)
        {
            return HttpHelper.GetResponseString(HttpHelper.CreatePostHttpResponse(url, parameters));
        }
        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }
        /// <summary>
        /// 验证证书
        /// </summary>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
        #endregion

        #region 异步

        public async Task<string> GetStringAsync(string url)
        {
            HttpClient hc = new HttpClient();
            //hc.DefaultRequestHeaders.Add("UserAgent", "contact@cnblogs.com");
            return await hc.GetStringAsync(url);
        }

        #endregion

        #region 下载
        /// <summary>
        /// 使用OutputStream.Write分块下载文件  
        /// </summary>
        /// <param name="filePath"></param>
        public static void WriteFileBlock(string filePath)
        {
            filePath = HttpContext.Current.Server.MapPath(filePath);
            if (!File.Exists(filePath)) return;
            using (FileStream fs=new FileStream(filePath,FileMode.Open)){
                var fileName = Path.GetFileName(filePath);
                HttpHelper.DownLoad(fs, fileName, ContentType.UnKnown);
            }
        }

        /// <summary>
        /// 分段下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名称(带后缀)</param>
        /// <param name="contentType">返回类型</param>
        public static void DownLoad(Stream stream,string fileName, string contentType)
        {
            //指定块大小   
            long chunkSize = 4096;
            //建立一个4K的缓冲区   
            byte[] buffer = new byte[chunkSize];
            //剩余的字节数   
            long dataToRead = 0;
            try
            {
                dataToRead = stream.Length;
                //添加Http头   
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpContext.Current.Server.UrlEncode(fileName));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());
                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接   
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }

        }

        public static void DownloadExcel(Stream stream, string fileName)
        {
            HttpHelper.DownLoad(stream,fileName,ContentType.Excel);
        }
        public static void DownloadWord(Stream stream, string fileName)
        {
            HttpHelper.DownLoad(stream, fileName, ContentType.Word);
        }
        #endregion
    }


    public class ContentType
    {
        public static string Excel
        {
            get
            {
                return "application/vnd.ms-excel";
            }
        }
        public static string Word
        {
            get
            {
                return "application/msword";
            }
        }
        public static string UnKnown
        {
            get
            {
                return "application/octet-stream";
            }
        }
    }
}
