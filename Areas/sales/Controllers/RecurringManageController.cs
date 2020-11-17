using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.sales.Controllers
{
    public class RecurringManageController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: sales/RecurringManage
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "ClientManage", new { @area = "sales" });
            }
            var recurringMasters = db.RecurringMasters.Where(a => a.RefCandidateId == id).Include(r => r.CandidateMaster);
            if(recurringMasters.ToList().Count==0)
            {
                TempData["Warning"] = "Details not Found!";
                return RedirectToAction("Index", "ClientManage", new { @area = "sales" });
            }
            return View(recurringMasters.ToList());
        }

        // GET: sales/RecurringManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringMaster recurringMaster = db.RecurringMasters.Find(id);
            if (recurringMaster == null)
            {
                return HttpNotFound();
            }
            return View(recurringMaster);
        }

        // GET: sales/RecurringManage/Create
        public ActionResult Create()
        {
            ViewBag.RefCandidateId = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName");
            return View();
        }

        // POST: sales/RecurringManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecurringId,DueDate,PaidDate,Amount,RefCandidateId,ReceivedIn,SendReminderEmail,PaymentStatus")] RecurringMaster recurringMaster)
        {
            if (ModelState.IsValid)
            {
                db.RecurringMasters.Add(recurringMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefCandidateId = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName", recurringMaster.RefCandidateId);
            return View(recurringMaster);
        }

        // GET: sales/RecurringManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringMaster recurringMaster = db.RecurringMasters.Find(id);
            if (recurringMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCandidateId = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName", recurringMaster.RefCandidateId);
            return View(recurringMaster);
        }

        // POST: sales/RecurringManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, string recIn, DateTime paid, string pstatus)
        {
            var recurringMaster = db.RecurringMasters.Find(id);
            recurringMaster.PaidDate = paid;
            recurringMaster.PaymentStatus = pstatus;
            recurringMaster.ReceivedIn = recIn;
            
            db.SaveChanges();

            var client = db.CandidateMasters.Find(recurringMaster.RefCandidateId);
            client.PaidAmount += recurringMaster.Amount;
            db.SaveChanges();

            return RedirectToAction("index", "RecurringManage", new { @id = id, @area = "Sales" });
        }

        // GET: sales/RecurringManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringMaster recurringMaster = db.RecurringMasters.Find(id);
            if (recurringMaster == null)
            {
                return HttpNotFound();
            }
            return View(recurringMaster);
        }

        // POST: sales/RecurringManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecurringMaster recurringMaster = db.RecurringMasters.Find(id);
            db.RecurringMasters.Remove(recurringMaster);
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
