using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using static ZbwsjdSupervise.Helper.GetHtml;

namespace ZbwsjdSupervise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpHeader header = new HttpHeader();
            header.accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            header.contentType = "application/x-www-form-urlencoded";             
            header.userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
            header.maxTry = 300;

            //ViewBag.html = HTMLHelper.GetHtmlWithGet("http://www.sina.com.cn", new CookieContainer(), header);
            //ViewBag.html = HTMLHelper.GetHtmlWithPost("http://dyxh.zbta.net/0533/login.php?gotopage=%2F0533%2F", new CookieContainer(), "gotopage=&dopost=login&adminstyle=newdedecms&userid=dyxh&pwd=dyxh2015&validate=2KQH&sm1=", header);

            CookieCollection cc= HTMLHelper.GetCookiWithGet("http://xxpt.wsjd.gov.cn/nhisportal/", new CookieContainer(), header) ;

            ViewBag.html = cc[0];

            CookieContainer ccn= new CookieContainer();
            ccn.Add(cc[0]);

            CookieCollection ccn2 = HTMLHelper.GetCookieByPost("http://xxpt.wsjd.gov.cn/nhisportal/login.do", "loginName=371915&password=151729abc&checkcode=74838", ccn, header);
            ViewBag.html = ccn2[0];


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}