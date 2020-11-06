using DTRS.Models;
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
            ViewBag.revenue = 1000;
            ViewBag.candidates = 1000;
            ViewBag.po = 5;
            ViewBag.leads = 10;

            ViewBag.targetedRevenue = 10000;
            ViewBag.archivedRevenue = 1;

            goalModel goal = new goalModel();
            goal.POTargeted = 10;
            goal.POArchived = 4;

            goal.archivedTargeted = 10;
            goal.revenueTargeted = 5000;

            goal.LeadArchived = 100;
            goal.LeadTargeted = 100;

            goal.CallArchived = 100;
            goal.CallTargeted = 400;

            ViewBag.goal = goal;
            return View();
        }
    }
}