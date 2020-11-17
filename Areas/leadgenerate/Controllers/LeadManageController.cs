using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.leadgenerate.Controllers
{
    public class LeadManageController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: leadgenerate/LeadManage
        public ActionResult Index()
        {
            var leadMasters = db.LeadMasters.Include(l => l.UserAccountDetail);
            return View(leadMasters.ToList());
        }

        // GET: leadgenerate/LeadManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadMaster leadMaster = db.LeadMasters.Find(id);
            if (leadMaster == null)
            {
                return HttpNotFound();
            }
            return View(leadMaster);
        }

        // GET: leadgenerate/LeadManage/Create
        public ActionResult Create()
        {
            var user = new sessionModel();
            user = Session["User"] as sessionModel;
            ViewBag.LeadBy = user.UserId;//new SelectList(db.UserAccountDetails, "UserId", "FullName");
            ViewBag.AssignedToSalesAssociate = new SelectList(db.UserAccountDetails.Where(a => a.RefRoleId == 11), "RocketName", "FullName");
            return View();
        }

        // POST: leadgenerate/LeadManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeadMaster leadMaster)
        {
            if (ModelState.IsValid)
            {
                db.LeadMasters.Add(leadMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeadBy = new SelectList(db.UserAccountDetails, "UserId", "FullName", leadMaster.LeadBy);
            return View(leadMaster);
        }

        // GET: leadgenerate/LeadManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadMaster leadMaster = db.LeadMasters.Find(id);
            if (leadMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeadBy = new SelectList(db.UserAccountDetails, "UserId", "FullName", leadMaster.LeadBy);
            return View(leadMaster);
        }

        // POST: leadgenerate/LeadManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lid,LeadDate,LeadBy,LeadType,LinkedInURL,CandidateName,CandidateNumber,LinkedInEmailid,CandidateLocation,TechnologyName,AssignedToSalesAssociate,Remarks,Feedback")] LeadMaster leadMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leadMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeadBy = new SelectList(db.UserAccountDetails, "UserId", "FullName", leadMaster.LeadBy);
            return View(leadMaster);
        }

        // GET: leadgenerate/LeadManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadMaster leadMaster = db.LeadMasters.Find(id);
            if (leadMaster == null)
            {
                return HttpNotFound();
            }
            return View(leadMaster);
        }

        // POST: leadgenerate/LeadManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeadMaster leadMaster = db.LeadMasters.Find(id);
            db.LeadMasters.Remove(leadMaster);
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
