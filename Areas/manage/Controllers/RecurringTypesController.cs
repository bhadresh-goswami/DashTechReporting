using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.manage.Controllers
{
    public class RecurringTypesController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: manage/RecurringTypes
        public ActionResult Index()
        {
            return View(db.RecurringTypes.ToList());
        }

        // GET: manage/RecurringTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringType recurringType = db.RecurringTypes.Find(id);
            if (recurringType == null)
            {
                return HttpNotFound();
            }
            return View(recurringType);
        }

        // GET: manage/RecurringTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manage/RecurringTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecurringTypeId,RecurringTitle,Amount,Installment")] RecurringType recurringType)
        {
            if (ModelState.IsValid)
            {
                db.RecurringTypes.Add(recurringType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recurringType);
        }

        // GET: manage/RecurringTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringType recurringType = db.RecurringTypes.Find(id);
            if (recurringType == null)
            {
                return HttpNotFound();
            }
            return View(recurringType);
        }

        // POST: manage/RecurringTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecurringTypeId,RecurringTitle,Amount,Installment")] RecurringType recurringType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recurringType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recurringType);
        }

        // GET: manage/RecurringTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecurringType recurringType = db.RecurringTypes.Find(id);
            if (recurringType == null)
            {
                return HttpNotFound();
            }
            return View(recurringType);
        }

        // POST: manage/RecurringTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecurringType recurringType = db.RecurringTypes.Find(id);
            db.RecurringTypes.Remove(recurringType);
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
