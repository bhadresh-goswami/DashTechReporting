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
    public class JobPortalController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        // GET: manage/JobPortal
        public ActionResult Index()
        {
            return View(db.JobPortalMasters.ToList());
        }

        // GET: manage/JobPortal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            if (jobPortalMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobPortalMaster);
        }

        // GET: manage/JobPortal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manage/JobPortal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PortalId,PortalTitle")] JobPortalMaster jobPortalMaster)
        {
            if (ModelState.IsValid)
            {
                db.JobPortalMasters.Add(jobPortalMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobPortalMaster);
        }

        // GET: manage/JobPortal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            if (jobPortalMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobPortalMaster);
        }

        // POST: manage/JobPortal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PortalId,PortalTitle")] JobPortalMaster jobPortalMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPortalMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobPortalMaster);
        }

        // GET: manage/JobPortal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            db.JobPortalMasters.Remove(jobPortalMaster);
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
