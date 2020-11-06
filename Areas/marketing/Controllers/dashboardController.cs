using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.marketing.Controllers
{
    public class dashboardController : Controller
    {
        // GET: marketing/dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}