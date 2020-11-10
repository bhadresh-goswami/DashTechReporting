using DTRS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.marketing.Controllers
{
    public class dashboardController : Controller
    {
        dashReportingEntities db = new dashReportingEntities();
        // GET: marketing/dashboard
        public ActionResult Index()
        {
            var date = DateTime.Now.Date;
            var data = db.CandidateMasters.ToList();
            return View(data);
        }

        public ActionResult StartMarketing(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("index");
                }
                var data = db.CandidateMasters.Find(id);
                ViewBag.data = data;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return View();
        }

        [HttpPost]
        public ActionResult StartMarketing(CandidateMarketingDetail model)//, HttpPostedFile OriginalResume, HttpPostedFile MarketingResume)
        {
            try
            {
                //if (OriginalResume != null)
                //{
                //    //Use Namespace called :  System.IO  
                //    string FileName = Path.GetFileNameWithoutExtension(OriginalResume.FileName);

                //    //To Get File Extension  
                //    string FileExtension = Path.GetExtension(OriginalResume.FileName);

                //    //Add Current Date To Attached File Name  
                //    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //    //Get Upload path from Web.Config file AppSettings.                      
                //    string _path = Path.Combine(Server.MapPath("~/Content/OriginalResumes/"), FileName);
                //    OriginalResume.SaveAs(_path);
                //}

                //if(MarketingResume !=null)
                //{
                //    //Use Namespace called :  System.IO  
                //    string FileName = Path.GetFileNameWithoutExtension(MarketingResume.FileName);

                //    //To Get File Extension  
                //    string FileExtension = Path.GetExtension(MarketingResume.FileName);

                //    //Add Current Date To Attached File Name  
                //    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //    //Get Upload path from Web.Config file AppSettings.                      
                //    string _path = Path.Combine(Server.MapPath("~/Content/MarketingResumes/"), FileName);
                //    MarketingResume.SaveAs(_path);
                //}

                //model.InsertedBy = "";
                sessionModel session = (sessionModel)Session["User"];
                model.InsertedBy = session.UserRocketName;
                model.MarketingStatus = "In Marketing";
                db.CandidateMarketingDetails.Add(model);
                db.SaveChanges();

                var salesCandiadate = db.CandidateMasters.Find(model.RefCandidateId);
                salesCandiadate.CandidateStatus = "In Marketing";
                db.SaveChanges();

                TempData["Message"] = "Candidate Marketing Detail Saved!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message + " \n" + ex.InnerException;
            }
            return RedirectToAction("Index");
        }


        public ActionResult EditMarketing(int? id)
        {

            try
            {
                if (id == null)
                {
                    return RedirectToAction("index");
                }
                var data = db.CandidateMarketingDetails.SingleOrDefault(asd => asd.RefCandidateId == id.Value);
                if (data != null)
                {

                    return View(data);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return View();
        }

        [HttpPost]
        public ActionResult EditMarketing(CandidateMarketingDetail candidate)
        {

            try
            {
                if (candidate == null)
                {
                    return RedirectToAction("index");
                }

                CandidateMarketingDetail newCandidate = db.CandidateMarketingDetails.SingleOrDefault(asd => asd.RefCandidateId == candidate.RefCandidateId);
                if (newCandidate != null)
                {
                    newCandidate.MarketingEmailId = candidate.MarketingEmailId;
                    newCandidate.MarketingContactNumber = candidate.MarketingContactNumber;
                    newCandidate.OriginalResume = candidate.OriginalResume;
                    newCandidate.MarketingResume = candidate.MarketingResume;
                    newCandidate.OtherRemarks = candidate.OtherRemarks;
                    newCandidate.LocationConcern = candidate.LocationConcern;
                    newCandidate.RequiredLocationList = candidate.RequiredLocationList;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return View();
        }

        public ActionResult CandidateDetails(int? Id)
        {


            return View();
        }

    }
}