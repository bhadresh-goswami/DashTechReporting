using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.technical.Controllers
{
    public class UserAccountDetailsController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: technical/UserAccountDetails
        public async Task<ActionResult> Index()
        {
            var userAccountDetails = db.UserAccountDetails.Where(asd=>asd.RefRoleId>= 8 && asd.RefRoleId<=10).Include(u => u.LocationMaster).Include(u => u.RoleMaster);
            return View(await userAccountDetails.ToListAsync());
        }

        // GET: technical/UserAccountDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = await db.UserAccountDetails.FindAsync(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            return View(userAccountDetail);
        }

        // GET: technical/UserAccountDetails/Create
        public ActionResult Create()
        {
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName");
            ViewBag.RefRoleId = new SelectList(db.RoleMasters.Where(asd=>asd.RoleTitle.Contains("Technical")).ToList(), "RoleId", "RoleTitle");
            return View();
        }

        // POST: technical/UserAccountDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,FullName,RocketName,EmailId,Password,RefLocationId,RefRoleId,UserImageUrl,IsActive,LastLogin,CompanyName")] UserAccountDetail userAccountDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserAccountDetails.Add(userAccountDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", userAccountDetail.RefLocationId);
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userAccountDetail.RefRoleId);
            return View(userAccountDetail);
        }

        // GET: technical/UserAccountDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = await db.UserAccountDetails.FindAsync(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", userAccountDetail.RefLocationId);
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userAccountDetail.RefRoleId);
            return View(userAccountDetail);
        }

        // POST: technical/UserAccountDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,FullName,RocketName,EmailId,Password,RefLocationId,RefRoleId,UserImageUrl,IsActive,LastLogin,CompanyName")] UserAccountDetail userAccountDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccountDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", userAccountDetail.RefLocationId);
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userAccountDetail.RefRoleId);
            return View(userAccountDetail);
        }

        // GET: technical/UserAccountDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = await db.UserAccountDetails.FindAsync(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            return View(userAccountDetail);
        }

        // POST: technical/UserAccountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserAccountDetail userAccountDetail = await db.UserAccountDetails.FindAsync(id);
            db.UserAccountDetails.Remove(userAccountDetail);
            await db.SaveChangesAsync();
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
