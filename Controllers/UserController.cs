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

            }
            catch (Exception ex)
            {

                
            }
            return View();
        }
    }
}