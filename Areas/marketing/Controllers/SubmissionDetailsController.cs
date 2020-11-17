using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.marketing.Controllers
{
    public class SubmissionDetailsController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: marketing/SubmissionDetails
        public ActionResult Index()
        {
            var submissionDetails = db.SubmissionDetails.Include(s => s.CandidateAssign);
            return View(submissionDetails.ToList());
        }

        // GET: marketing/SubmissionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmissionDetail submissionDetail = db.SubmissionDetails.Find(id);
            if (submissionDetail == null)
            {
                return HttpNotFound();
            }
            return View(submissionDetail);
        }

        // GET: marketing/SubmissionDetails/Create
        public ActionResult Create(int? id)
        {
            //ViewBag.RefAssignedId = new SelectList(db.CandidateAssigns, "AssignedId", "AssignedId");
            ViewBag.aid = id;
            return View();
        }

        // POST: marketing/SubmissionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmissionDetail submissionDetail)
        {
            try
            {
                submissionDetail.StatusOfSubmission = "In Follow Up";
                db.SubmissionDetails.Add(submissionDetail);
                db.SaveChanges();
                TempData["Message"] = "Submission Done!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            //ViewBag.RefAssignedId = new SelectList(db.CandidateAssigns, "AssignedId", "AssignedId", submissionDetail.RefAssignedId);
            return View(submissionDetail);
        }

        // GET: marketing/SubmissionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmissionDetail submissionDetail = db.SubmissionDetails.Find(id);
            if (submissionDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefAssignedId = new SelectList(db.CandidateAssigns, "AssignedId", "AssignedId", submissionDetail.RefAssignedId);
            return View(submissionDetail);
        }

        // POST: marketing/SubmissionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubmissionId,Date,VendorName,VendorContactNumber,VendorEmailId,VendorCompanyName,ClientName,PerHourRate,JobTitle,JobLocation,Remarks,RefAssignedId,FollowUpDate,StatusOfSubmission")] SubmissionDetail submissionDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submissionDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefAssignedId = new SelectList(db.CandidateAssigns, "AssignedId", "AssignedId", submissionDetail.RefAssignedId);
            return View(submissionDetail);
        }

        // GET: marketing/SubmissionDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmissionDetail submissionDetail = db.SubmissionDetails.Find(id);
            if (submissionDetail == null)
            {
                return HttpNotFound();
            }
            return View(submissionDetail);
        }

        // POST: marketing/SubmissionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmissionDetail submissionDetail = db.SubmissionDetails.Find(id);
            db.SubmissionDetails.Remove(submissionDetail);
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
