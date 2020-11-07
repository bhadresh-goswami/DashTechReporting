using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.admin.Controllers
{
    public class LocationsController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: admin/Locations
        public ActionResult Index()
        {
            return View(db.LocationMasters.ToList());
        }

        // GET: admin/Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // GET: admin/Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,LocationName")] LocationMaster locationMaster)
        {
            if (ModelState.IsValid)
            {
                db.LocationMasters.Add(locationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationMaster);
        }

        // GET: admin/Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // POST: admin/Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,LocationName")] LocationMaster locationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationMaster);
        }

        // GET: admin/Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // POST: admin/Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            db.LocationMasters.Remove(locationMaster);
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
