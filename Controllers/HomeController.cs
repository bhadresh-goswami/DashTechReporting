using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult admin()
        {
            sessionModel m = new sessionModel();
            m.UserId = 1;
            m.UserRocketName = "bhadresh.gosai";
            m.UserRole = "admin";
            m.defaultUrl = Url.Action("index","dashboard",new { @area = "admin" });
            Session["User"] = m;
            Session.Timeout = 50;
           
            return Redirect(m.defaultUrl);
        }
        public ActionResult recruiter()
        {
            sessionModel m = new sessionModel();
            m.UserId = 5;
            m.UserRocketName = "ronak.g";
            m.UserRole = "Recruiter";
            m.defaultUrl = Url.Action("index", "recruiter", new { @area = "marketing" });
            Session["User"] = m;
            Session.Timeout = 50;

            return Redirect(m.defaultUrl);
        }
    }

}
