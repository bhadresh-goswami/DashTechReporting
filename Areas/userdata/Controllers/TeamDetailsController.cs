using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;
using WebGrease.Css.Extensions;

namespace DTRS.Areas.userdata.Controllers
{
    public class TeamDetailsController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: userdata/TeamDetails
        public ActionResult Index()
        {
            var teamDetails = db.TeamDetails.Include(t => t.UserAccountDetail).Include(t => t.UserAccountDetail1).Include(t => t.UserAccountDetail2);
            return View(teamDetails.ToList());
        }

        // GET: userdata/TeamDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            if (teamDetail == null)
            {
                return HttpNotFound();
            }
            return View(teamDetail);
        }

        // GET: userdata/TeamDetails/Create
        public ActionResult Create()
        {
            var data = db.UserAccountDetails;
            var listMember = new List<UserAccountDetail>();
            foreach (var item in data)
            {
                var teamData = db.TeamDetails.SingleOrDefault(a => a.Member == item.UserId);
                if (teamData == null)
                {
                    listMember.Add(item);
                }
            }
            ViewBag.Member = new SelectList(listMember.Where(a => a.RefRoleId == 4), "UserId", "FullName");
            ViewBag.TeamLead = new SelectList(data.Where(a => a.RefRoleId == 3), "UserId", "FullName");
            ViewBag.TeamManager = new SelectList(data.Where(a => a.RefRoleId == 2), "UserId", "FullName");
            return View();
        }

        // POST: userdata/TeamDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,Member,TeamLead,TeamManager,Department")] TeamDetail teamDetail)
        {
            if (ModelState.IsValid)
            {
                db.TeamDetails.Add(teamDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Member = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.Member);
            ViewBag.TeamLead = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamLead);
            ViewBag.TeamManager = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamManager);
            return View(teamDetail);
        }

        // GET: userdata/TeamDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            if (teamDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.Member);
            ViewBag.TeamLead = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamLead);
            ViewBag.TeamManager = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamManager);
            return View(teamDetail);
        }

        // POST: userdata/TeamDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,Member,TeamLead,TeamManager,Department")] TeamDetail teamDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Member = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.Member);
            ViewBag.TeamLead = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamLead);
            ViewBag.TeamManager = new SelectList(db.UserAccountDetails, "UserId", "FullName", teamDetail.TeamManager);
            return View(teamDetail);
        }

        // GET: userdata/TeamDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            if (teamDetail == null)
            {
                return HttpNotFound();
            }
            return View(teamDetail);
        }

        // POST: userdata/TeamDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamDetail teamDetail = db.TeamDetails.Find(id);
            db.TeamDetails.Remove(teamDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
