using DTRS.Models;
using DTRS.Models.visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string emailid, string password)
        {
            try
            {
                SignIn signin = new SignIn();
                signin.UserName = emailid;
                signin.Password = password;
                object obj = signin._SignIn();
                if (obj.GetType() == typeof(sessionModel))
                {
                    Session["User"] = obj as sessionModel;
                    return Redirect((obj as sessionModel).defaultUrl);
                }
                else
                {
                    ViewBag.Msg = obj;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();

            }
            
        }
    }
}