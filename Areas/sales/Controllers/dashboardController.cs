using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.sales.Controllers
{
    public class dashboardController : Controller
    {
        dashReportingEntities db = new dashReportingEntities();
        // GET: sales/dashboard
        public ActionResult Index()
        {
            var date = DateTime.Now.Date;
            var data = db.CandidateMasters.ToList();
            return View(data);
        
        }
    }
}