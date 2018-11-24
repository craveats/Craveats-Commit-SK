using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize, Tls]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionManager.RegisterSessionActivity();
            ViewBag.Message = (Session["AreaVisited"] != null ?
                ("Last visited: " + Session["AreaVisited"].ToString() + " ") : "");
            Session["AreaVisited"] = "Home/Index";
            return View();
        }

        public ActionResult About()
        {
            SessionManager.RegisterSessionActivity();
            ViewBag.Message = (Session["AreaVisited"]!=null ? 
                ("Last visited: " + Session["AreaVisited"].ToString()+ " ") : "") + "Your application description page.";
            Session["AreaVisited"] = "Home/About";
            return View();
        }

        public ActionResult Contact()
        {
            SessionManager.RegisterSessionActivity();
            ViewBag.Message = (Session["AreaVisited"] != null ?
                ("Last visited: " + Session["AreaVisited"].ToString() + " ") : "") + "Your contact page.";
            Session["AreaVisited"] = "Home/Contact";
            return View();
        }
    }
}