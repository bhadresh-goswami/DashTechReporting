using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.leadgenerate.Controllers
{
    public class dashboardController : Controller
    {
        // GET: leadgenerate/dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}