using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.technical.Controllers
{
    public class TeamController : Controller
    {              
            private dashReportingEntities db = new dashReportingEntities();

            // GET: manage/Team
            public ActionResult Index()
            {
                return View(db.TeamDetails.ToList());
            }

            // GET: manage/Team/Details/5
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

            // GET: manage/Team/Create
            public ActionResult Create()
            {
            ViewBag.Member = new SelectList(db.UserAccountDetails.Where(u=>u.RefRoleId == 8).ToList(), "UserId", "FullName");
            ViewBag.TeamLead = new SelectList(db.UserAccountDetails.Where(u => u.RefRoleId == 9).ToList(), "UserId", "FullName");
            ViewBag.TeamManager = new SelectList(db.UserAccountDetails.Where(u => u.RefRoleId == 10).ToList(), "UserId", "FullName");
            ViewBag.Department = new SelectList(db.DepartmentMasters, "DepartmentName", "DepartmentName");

            return View();
            }

            // POST: manage/Team/Create
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

                return View(teamDetail);
            }

            // GET: manage/Team/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TeamDetail teamDetail = db.TeamDetails.Find(id);
            ViewBag.Member = new SelectList(db.UserAccountDetails.Where(u => u.RefRoleId == 8).ToList(), "UserId", "FullName");
            ViewBag.TeamLead = new SelectList(db.UserAccountDetails.Where(u => u.RefRoleId == 9).ToList(), "UserId", "FullName");
            ViewBag.TeamManager = new SelectList(db.UserAccountDetails.Where(u => u.RefRoleId == 10).ToList(), "UserId", "FullName");
            ViewBag.Department = new SelectList(db.DepartmentMasters, "DepartmentName", "DepartmentName");

            if (teamDetail == null)
                {
                    return HttpNotFound();
                }
                return View(teamDetail);
            }

            // POST: manage/Team/Edit/5
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
                return View(teamDetail);
            }

            // GET: manage/Team/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

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
