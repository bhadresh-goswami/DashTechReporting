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
    public class SalesServiceController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: manage/SalesService
        public ActionResult Index()
        {
            return View(db.SalesServiceMasters.ToList());
        }

        // GET: manage/SalesService/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServiceMaster salesServiceMaster = db.SalesServiceMasters.Find(id);
            if (salesServiceMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesServiceMaster);
        }

        // GET: manage/SalesService/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: manage/SalesService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,ServiceName")] SalesServiceMaster salesServiceMaster)
        {
            if (ModelState.IsValid)
            {
                db.SalesServiceMasters.Add(salesServiceMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesServiceMaster);
        }

        // GET: manage/SalesService/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServiceMaster salesServiceMaster = db.SalesServiceMasters.Find(id);
            if (salesServiceMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesServiceMaster);
        }

        // POST: manage/SalesService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceName")] SalesServiceMaster salesServiceMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesServiceMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesServiceMaster);
        }

        // GET: manage/SalesService/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServiceMaster salesServiceMaster = db.SalesServiceMasters.Find(id);
            if (salesServiceMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesServiceMaster);
        }

        // POST: manage/SalesService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesServiceMaster salesServiceMaster = db.SalesServiceMasters.Find(id);
            db.SalesServiceMasters.Remove(salesServiceMaster);
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
