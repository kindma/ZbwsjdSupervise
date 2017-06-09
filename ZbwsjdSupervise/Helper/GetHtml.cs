using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace ZbwsjdSupervise.Helper
{
    public class GetHtml
    {
        public class HTMLHelper
        {
            /// <summary>
            /// 获取CooKie
            /// </summary>
            /// <param name="loginUrl"></param>
            /// <param name="postdata"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static CookieCollection GetCookieByPost(string loginUrl, string postdata, CookieContainer ccn, HttpHeader header)
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
              
                    
                    request = (HttpWebRequest)WebRequest.Create(loginUrl);
                    request.Method = "POST";
                    request.ContentType = header.contentType;
                    byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);
                    request.ContentLength = postdatabyte.Length;
                    request.AllowAutoRedirect = false;
                    request.CookieContainer = ccn;
                    request.KeepAlive = true;

                    //提交请求
                    Stream stream;
                    stream = request.GetRequestStream();
                    stream.Write(postdatabyte, 0, postdatabyte.Length);
                    stream.Close();

                    //接收响应
                    response = (HttpWebResponse)request.GetResponse();
                    


                    //接收响应

                    CookieCollection cc = response.Cookies;

                    request.Abort();
                    response.Close();

                    return cc;

               
            }

            /// <summary>
            /// 获取COOKIE用GET
            /// </summary>
            /// <param name="getUrl"></param>
            /// <param name="cookieContainer"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static CookieCollection GetCookiWithGet(string getUrl, CookieContainer cookieContainer, HttpHeader header)
            {
                Thread.Sleep(1000);
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
               

                    httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);

                    //cookie传值 
                    httpWebRequest.CookieContainer = cookieContainer;

                    httpWebRequest.Method = "GET";
                    httpWebRequest.Referer = getUrl;

                    //header传值
                    httpWebRequest.ContentType = header.contentType;
                    httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;
                    httpWebRequest.Accept = header.accept;
                    httpWebRequest.UserAgent = header.userAgent;

                    //得到response
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();


                    //接收响应

                    CookieCollection cc= httpWebResponse.Cookies;

                    httpWebRequest.Abort();
                    httpWebResponse.Close();

                    return cc;
                 
            }


            /// <summary>
            /// 获取html用GET
            /// </summary>
            /// <param name="getUrl"></param>
            /// <param name="cookieContainer"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static string GetHtmlWithGet(string getUrl, CookieContainer cookieContainer, HttpHeader header)
            {
                Thread.Sleep(1000);
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
                try
                {
                    httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);

                    //cookie传值 
                    httpWebRequest.CookieContainer = cookieContainer;

                    httpWebRequest.Method = "GET";
                    httpWebRequest.Referer = getUrl;

                    //header传值
                    httpWebRequest.ContentType = header.contentType;
                    httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;                    
                    httpWebRequest.Accept = header.accept;
                    httpWebRequest.UserAgent = header.userAgent;
                    
                    //得到response
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //得到reponse流
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    //用流读器读上面的流
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    //将流转为HTML
                    string html = streamReader.ReadToEnd();
                    //释放
                    streamReader.Close();
                    responseStream.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();

                    return html;
                }
                catch (Exception e)
                {
                    if (httpWebRequest != null) httpWebRequest.Abort();
                    if (httpWebResponse != null) httpWebResponse.Close();
                    return string.Empty;
                }
            }

            /// <summary>
            /// 获取html用POST
            /// </summary>
            /// <param name="getUrl"></param>
            /// <param name="cookieContainer"></param>
            /// <param name="header"></param>
            /// <returns></returns>
            public static string GetHtmlWithPost(string getUrl, CookieContainer cookieContainer,string postData,  HttpHeader header)
            {
                Thread.Sleep(1000);
                HttpWebRequest httpWebRequest = null;
                HttpWebResponse httpWebResponse = null;
                try
                {
                    httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);

                    httpWebRequest.Method = "POST";
                    httpWebRequest.Referer = getUrl;
                    httpWebRequest.AllowAutoRedirect = true;
                    httpWebRequest.KeepAlive = true;
                    

                    //cookie传值 
                    httpWebRequest.CookieContainer = cookieContainer;

                    //header传值
                    httpWebRequest.ContentType = header.contentType;
                    httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;
                    httpWebRequest.Accept = header.accept;
                    httpWebRequest.UserAgent = header.userAgent;

                    //设置POST内容
                    httpWebRequest.ContentType = header.contentType;
                    byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
                    httpWebRequest.ContentLength = postdatabyte.Length;

                    //提交请求
                    Stream stream;
                    stream = httpWebRequest.GetRequestStream(); //得到请求流
                    stream.Write(postdatabyte, 0, postdatabyte.Length); //将POST数据写到流中
                    stream.Close();


                    //得到response
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //得到reponse流
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    //用流读器读上面的流
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    //将流转为HTML
                    string html = streamReader.ReadToEnd();
                    //释放
                    streamReader.Close();
                    responseStream.Close();
                    httpWebRequest.Abort();
                    httpWebResponse.Close();

                    return html;
                }
                catch (Exception e)
                {
                    if (httpWebRequest != null) httpWebRequest.Abort();
                    if (httpWebResponse != null) httpWebResponse.Close();
                    return string.Empty;
                }
            }
        }

        public class HttpHeader
        {
            public string contentType { get; set; }

            public string accept { get; set; }

            public string userAgent { get; set; }

            public string method { get; set; }

            public int maxTry { get; set; }
        }
    }
}