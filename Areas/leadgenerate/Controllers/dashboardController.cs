using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.leadgenerate.Controllers
{
    public class dashboardController : Controller
    {

        dashReportingEntities db = new dashReportingEntities();
        // GET: leadgenerate/dashboard
        public ActionResult Index()
        {
            var candidate = db.CandidateMasters.ToList();
            List<CandidateMaster> candidates = new List<CandidateMaster>();
            foreach (var item in candidate)
            {
                if (db.LeadMasters.Where(a => a.CandidateNumber == item.MobileNumber).ToList().Count > 0)
                {
                    candidates.Add(item);
                }
            }
            return View(candidates);
        }
    }
}