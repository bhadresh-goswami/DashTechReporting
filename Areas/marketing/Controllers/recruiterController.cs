using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.marketing.Controllers
{
    public class recruiterController : Controller
    {
        DTRSDatabaseEntities db = new DTRSDatabaseEntities();
        // GET: marketing/recruiter
        public ActionResult Index(DateTime? date)
        {
            sessionModel model = Session["User"] as sessionModel;

            var user = db.UserAccountDetails.SingleOrDefault(a=>a.UserId == model.UserId);
            var team = db.TeamDetails.SingleOrDefault(a => a.Member == user.UserId);
            DateTime dt = DateTime.Now.Date;
            if (date != null)
            {
                dt = (DateTime) date;
            }
            var data = db.CandidateAssigns.Where(a=>a.RefTeamId == team.TeamId);
            return View(data);
        }
    }
}