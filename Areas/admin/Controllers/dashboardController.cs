using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.admin.Controllers
{
    public class dashboardController : Controller
    {
        // GET: admin/dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}