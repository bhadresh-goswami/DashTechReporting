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
    public class ClientManageController : Controller
    {
        private dashReportingEntities db = new dashReportingEntities();

        // GET: sales/ClientManage
        public ActionResult Index()
        {
            var candidateMasters = db.CandidateMasters.Include(c => c.RecurringType).Include(c => c.UserAccountDetail).Include(c => c.SalesServiceMaster).Include(c => c.TechnologyMaster);
            return View(candidateMasters.ToList());
        }

        // GET: sales/ClientManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }
        [HttpGet]
        public int getinstallments(int? id)
        {
            if (id == null)
            {
                return -1;
            }
            RecurringType ret = db.RecurringTypes.Find(id);

            return ret.Installment;
        }

        // GET: sales/ClientManage/Create
        public ActionResult Create()
        {
            ViewBag.RefRecurringTypeId = new SelectList(db.RecurringTypes, "RecurringTypeId", "RecurringTitle");
            ViewBag.RefSalesAssociate = new SelectList(db.UserAccountDetails, "UserId", "FullName");
            ViewBag.RefServiceId = new SelectList(db.SalesServiceMasters, "ServiceId", "ServiceName");
            ViewBag.TechnologyId = new SelectList(db.TechnologyMasters, "TechId", "TechTitle");
            return View();
        }

        // POST: sales/ClientManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CandidateMaster candidateMaster, DateTime? PartialDate, decimal? PartialAmount, string ReceivedIn = "")
        {
            sessionModel session = new sessionModel() ;
            if (Session["User"] != null)
            {
                session = Session["User"] as sessionModel;
            }
            //CandidateStatus,
            //MarketingStartDate
            //RefSalesAssociate
            candidateMaster.CandidateStatus = "Sales";
            candidateMaster.MarketingStartDate = null;
            candidateMaster.RefSalesAssociate = session.UserId;
            db.CandidateMasters.Add(candidateMaster);
            db.SaveChanges();

            var recAmount = new RecurringMaster()
            {
                Amount = candidateMaster.PaidAmount,
                DueDate = candidateMaster.Date,
                PaidDate = candidateMaster.Date,
                PaymentStatus = "Paid",
                ReceivedIn = ReceivedIn,
                RefCandidateId = candidateMaster.CandidateId,
                SendReminderEmail = false
            };
            db.RecurringMasters.Add(recAmount);
            db.SaveChanges();
            if (PartialAmount != null && PartialDate != null)
            {

                recAmount = new RecurringMaster()
                {
                    Amount = candidateMaster.TotalAmount - candidateMaster.PaidAmount,
                    DueDate = (DateTime)PartialDate,
                    PaidDate = null,
                    PaymentStatus = "Un-Paid",
                    ReceivedIn = "",
                    RefCandidateId = candidateMaster.CandidateId,
                    SendReminderEmail = false
                };

                db.RecurringMasters.Add(recAmount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //if (Session["reccuring"] != null)
            //{
            //    int id = candidateMaster.CandidateId;
            //    var recs = new List<RecurringMaster>();
            //    for (int i = 0; i < recs.Count; i++)
            //    {
            //        recs[i].RefCandidateId = id;
            //    }
            //    db.RecurringMasters.AddRange(recs);
            //    db.SaveChanges();
            //}
            //Session.Remove("recurring");
            var rec = db.RecurringTypes.Find(candidateMaster.RefRecurringTypeId);
            if (rec.Installment > 1)
            {
                decimal paidAmt = candidateMaster.PaidAmount;
                var firstPaidInstallment = paidAmt;

                decimal partial = 0;
                if (rec.Installment - 1 > 0)
                {
                    partial = rec.Amount;
                }


                var recs = new List<RecurringMaster>();
                DateTime dt = DateTime.Now;
                for (int i = 0; i < rec.Installment - 1; i++)
                {
                    dt = dt.AddDays(30);
                    var recurring = new RecurringMaster()
                    {
                        Amount = partial,
                        DueDate = dt,
                        PaidDate = null,
                        PaymentStatus = "Un-Paid",
                        ReceivedIn = "",
                        RefCandidateId = candidateMaster.CandidateId
                    };
                    recs.Add(recurring);
                }
                db.RecurringMasters.AddRange(recs);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
            //ViewBag.RefRecurringTypeId = new SelectList(db.RecurringTypes, "RecurringTypeId", "RecurringTitle", candidateMaster.RefRecurringTypeId);
            //ViewBag.RefSalesAssociate = new SelectList(db.UserAccountDetails, "UserId", "FullName", candidateMaster.RefSalesAssociate);
            //ViewBag.RefServiceId = new SelectList(db.SalesServiceMasters, "ServiceId", "ServiceName", candidateMaster.RefServiceId);
            //ViewBag.TechnologyId = new SelectList(db.TechnologyMasters, "TechId", "TechTitle", candidateMaster.TechnologyId);
            //return View(candidateMaster);
        }

        public JsonResult getRecurring()
        {
            List<RecurringMaster> list = new List<RecurringMaster>();
            if (Session["recurring"] != null)
            {
                list = Session["recurring"] as List<RecurringMaster>;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public JsonResult SaveRecurring(decimal amt, DateTime date)
        {
            try
            {
                RecurringMaster recurring = new RecurringMaster();
                List<RecurringMaster> list = new List<RecurringMaster>();
                if (Session["recurring"] != null)
                {
                    list = Session["recurring"] as List<RecurringMaster>;
                }
                recurring.Amount = amt;
                recurring.DueDate = date;
                recurring.ReceivedIn = "";
                recurring.PaidDate = null;
                recurring.RefCandidateId = -1;
                recurring.SendReminderEmail = false;
                recurring.PaymentStatus = "Un-Paid";

                list.Add(recurring);

                Session["recurring"] = list;

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new Dictionary<string, string>();
                data["Error"] = ex.Message;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: sales/ClientManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefRecurringTypeId = new SelectList(db.RecurringTypes, "RecurringTypeId", "RecurringTitle", candidateMaster.RefRecurringTypeId);
            ViewBag.RefSalesAssociate = new SelectList(db.UserAccountDetails, "UserId", "FullName", candidateMaster.RefSalesAssociate);
            ViewBag.RefServiceId = new SelectList(db.SalesServiceMasters, "ServiceId", "ServiceName", candidateMaster.RefServiceId);
            ViewBag.TechnologyId = new SelectList(db.TechnologyMasters, "TechId", "TechTitle", candidateMaster.TechnologyId);
            return View(candidateMaster);
        }

        // POST: sales/ClientManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CandidateMaster candidateMaster, DateTime? PartialDate, decimal? PartialAmount, string ReceivedIn = "")
        {
            //CandidateStatus,
            //MarketingStartDate
            //RefSalesAssociate
            candidateMaster.CandidateStatus = "In Progress";
            candidateMaster.MarketingStartDate = null;
            candidateMaster.RefSalesAssociate = 2;
            //db.CandidateMasters.Add(candidateMaster);
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        // GET: sales/ClientManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // POST: sales/ClientManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            db.CandidateMasters.Remove(candidateMaster);
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
