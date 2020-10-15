using DTRS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DTRS.Models;
using DTRS.Models.Submission;

namespace DTRS.Areas.Recruiter.Controllers
{
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
                model.SDate = DateTime.Now.Date;
                model.STime = DateTime.Now.TimeOfDay;
                model.SBy = "bhadresh.gosai";
                db.SubmissionMasters.Add(model);
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