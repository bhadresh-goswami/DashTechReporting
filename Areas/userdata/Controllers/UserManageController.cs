using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.userdata.Controllers
{
    public class UserManageController : Controller
    {
        private DTRSDatabaseEntities db = new DTRSDatabaseEntities();

        [HttpPost]
        public ActionResult ChangePassword(string old, string newp, int? id)
        {
            try
            {
                UserAccountDetail user = new UserAccountDetail();
                if (id == null)
                {
                    sessionModel session = (sessionModel)Session["User"];
                    user = db.UserAccountDetails.Find(session.UserId);
                }
                else
                    user = db.UserAccountDetails.Find(id);

                if (user.Password == old)
                {
                    user.Password = newp;
                    db.SaveChanges();
                    TempData["Success"] = "User Password Changed!";
                }
                else TempData["Warning"] = "Old Password not match!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return RedirectToAction("Index");
        }
        // GET: userdata/UserManage
        public ActionResult Index()
        {
            var userAccountDetails = db.UserAccountDetails.Include(u => u.LocationMaster).Include(u => u.RoleMaster);
            return View(userAccountDetails.ToList());
        }

        // GET: userdata/UserManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = db.UserAccountDetails.Find(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            return View(userAccountDetail);
        }

        // GET: userdata/UserManage/Create
        public ActionResult Create()
        {
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName");
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle");
            return View();
        }

        // POST: userdata/UserManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FullName,RocketName,EmailId,Password,RefLocationId,RefRoleId,UserImageUrl,IsActive,LastLogin,CompanyName")] UserAccountDetail userAccountDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserAccountDetails.Add(userAccountDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", userAccountDetail.RefLocationId);
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userAccountDetail.RefRoleId);
            return View(userAccountDetail);
        }

        // GET: userdata/UserManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = db.UserAccountDetails.Find(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", userAccountDetail.RefLocationId);
            ViewBag.RefRoleId = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userAccountDetail.RefRoleId);
            return View(userAccountDetail);
        }

        // POST: userdata/UserManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAccountDetail userAccountDetail)
        {
            try
            {
                userAccountDetail.LastLogin = null;
                
                db.Entry(userAccountDetail).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Data Saved!";

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return RedirectToAction("Index");

        }

        // GET: userdata/UserManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountDetail userAccountDetail = db.UserAccountDetails.Find(id);
            if (userAccountDetail == null)
            {
                return HttpNotFound();
            }
            return View(userAccountDetail);
        }

        // POST: userdata/UserManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccountDetail userAccountDetail = db.UserAccountDetails.Find(id);
            db.UserAccountDetails.Remove(userAccountDetail);
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
