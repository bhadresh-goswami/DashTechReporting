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
    public class TaskTitleMastersController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: technical/TaskTitleMasters
        public async Task<ActionResult> Index()
        {
            return View(await db.TaskTitleMasters.ToListAsync());
        }

        // GET: technical/TaskTitleMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = await db.TaskTitleMasters.FindAsync(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // GET: technical/TaskTitleMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: technical/TaskTitleMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaskName,IsActive")] TaskTitleMaster taskTitleMaster)
        {

            var user = new sessionModel();
            user = Session["User"] as sessionModel;
            taskTitleMaster.CDate = DateTime.Now;
            taskTitleMaster.CreatedBy = user.UserRocketName;
            db.TaskTitleMasters.Add(taskTitleMaster);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: technical/TaskTitleMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = await db.TaskTitleMasters.FindAsync(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // POST: technical/TaskTitleMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, [Bind(Include = "TaskName,IsActive")] TaskTitleMaster taskTitleMaster)
        {

            taskTitleMaster.TaskTitleID = id.Value;
            db.Entry(taskTitleMaster).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: technical/TaskTitleMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = await db.TaskTitleMasters.FindAsync(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // POST: technical/TaskTitleMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaskTitleMaster taskTitleMaster = await db.TaskTitleMasters.FindAsync(id);
            db.TaskTitleMasters.Remove(taskTitleMaster);
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
