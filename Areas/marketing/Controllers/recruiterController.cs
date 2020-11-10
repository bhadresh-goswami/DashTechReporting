using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.marketing.Controllers
{
    public class recruiterController : Controller
    {
        dashReportingEntities db = new dashReportingEntities();
        // GET: marketing/recruiter
        public ActionResult Index(DateTime? date)
        {
            DateTime dt = DateTime.Now.Date;
            if (date != null)
            {
                dt = (DateTime) date;
            }
            //var data = db.CandidateMasters
            return View();
        }
    }
}