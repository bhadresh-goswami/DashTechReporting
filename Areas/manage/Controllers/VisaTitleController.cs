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
    public class VisaTitleController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: manage/VisaTitle
        public ActionResult Index()
        {
            return View(db.VisaTitleMasters.ToList());
        }

        // GET: manage/VisaTitle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTitleMaster visaTitleMaster = db.VisaTitleMasters.Find(id);
            if (visaTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(visaTitleMaster);
        }

        // GET: manage/VisaTitle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manage/VisaTitle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VisaId,VisaTitle")] VisaTitleMaster visaTitleMaster)
        {
            if (ModelState.IsValid)
            {
                db.VisaTitleMasters.Add(visaTitleMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visaTitleMaster);
        }

        // GET: manage/VisaTitle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTitleMaster visaTitleMaster = db.VisaTitleMasters.Find(id);
            if (visaTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(visaTitleMaster);
        }

        // POST: manage/VisaTitle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisaId,VisaTitle")] VisaTitleMaster visaTitleMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visaTitleMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visaTitleMaster);
        }

        // GET: manage/VisaTitle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisaTitleMaster visaTitleMaster = db.VisaTitleMasters.Find(id);
            if (visaTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(visaTitleMaster);
        }

        // POST: manage/VisaTitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisaTitleMaster visaTitleMaster = db.VisaTitleMasters.Find(id);
            db.VisaTitleMasters.Remove(visaTitleMaster);
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
