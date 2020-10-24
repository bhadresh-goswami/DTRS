using DTRS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DTRS.Models;
using DTRS.Models.Submission;
using static DTRS.FilterConfig;

namespace DTRS.Areas.Recruiter.Controllers
{
    [_AuthenticationFilter]
    public class DashboardController : MasterCodeController
    {
        public DashboardController()
        {
            db = new dbReportingSystemEntities();
        }
        // GET: Recruiter/Dashboard
        public ActionResult Index()
        {
            return View(db.SubmissionMasters);
        }

        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubmissionMaster model)
        {
            try
            {
                int id = int.Parse(Session["userId"].ToString());
                var user = db.UserLoginMasters.Find(id);
                model.SDate = DateTime.Now.Date;
                model.STime = DateTime.Now.TimeOfDay;
                model.SBy = Session["name"].ToString();
                model.Location = user.Location;
                model.InterviewDate = null;
                model.InterviewFeedBack = "";
                model.InterviewStatus = "Interview Pending";
                model.InterviewTime = null;
                model.AssingedTo = "";
                db.SubmissionMasters.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddInterview(int? sid)
        {
            if (sid == null)
            {
                TempData["Warning"] = "Please selecte the Submission!";
                return RedirectToAction("Index");
            }
            int id = int.Parse(Session["userId"].ToString());
            var user = db.SubmissionMasters.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult AddInterview(int? SId, SubmissionMaster model,string AssingedToOther)
        {
           
            try
            {
                var submission = db.SubmissionMasters.Find(SId);
               
                submission.InterviewDate = model.InterviewDate;
                submission.InterviewFeedBack = "";
                submission.InterviewStatus = "Interview Scheduled";
                submission.InterviewTime = model.InterviewTime;

                var toemailidlist = "";
                if (AssingedToOther != "")
                {
                    submission.AssingedTo = AssingedToOther;
                }
                else
                {
                    submission.AssingedTo = model.AssingedTo;
                    var user = db.UserLoginMasters.SingleOrDefault(a => a.RocketUserName == AssingedToOther);
                    toemailidlist += user.EmailId;
                }
                
                db.SaveChanges();
                TempData["Message"] = "Interview Schedule!";
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(SubmissionMaster model)
        {
            try
            {
                var data = db.SubmissionMasters.Find(model.SId);

                //model.SDate = data.SDate;
                //model.STime = data.STime;
                //model.SBy = Session["name"].ToString();
                //data = model;
                //db.SubmissionMasters.Add(model);
                //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                data.ClientName = model.ClientName;
                data.ContactEmailId = model.ContactEmailId;
                data.Rate = model.Rate;
                data.VendorCompanyName = model.VendorCompanyName;
                data.VendorName = model.VendorName;
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}