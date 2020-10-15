using DTRS.Controllers;
using DTRS.Models;
using DTRS.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.MarketingManager.Controllers
{
    public class DashboardController : MasterCodeController
    {
        public DashboardController()
        {
            db = new dbReportingSystemEntities();
        }
        // GET: MarketingManager/Dashboard
        public ActionResult Index()
        {
            List<DashboardSubmissionModel> submissionModels = new List<DashboardSubmissionModel>();

            try
            {
                foreach (var item in db.UserLoginMasters.Where(a => a.UserRole == 3))
                {
                    var name = item.RocketUserName;
                    var totalSubmission = db.SubmissionMasters.Where(a => a.SBy == name).ToList().Count;
                    var totalInterview = db.SubmissionMasters.Where(a => a.SBy == name).ToList().Count;
                    var totalPO = db.SubmissionMasters.Where(a => a.SBy == name).ToList().Count;
                    var totalCandidates = db.SubmissionMasters.Where(a => a.SBy == name).ToList().Count;
                    DashboardSubmissionModel model = new DashboardSubmissionModel();
                    model.TCCount = totalCandidates;
                    model.TICount = totalInterview;
                    model.TPOCount = totalPO;
                    model.TSCount = totalSubmission;
                    model.Name = name;
                    submissionModels.Add(model);
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            ViewBag.Name = "Akash Shah";
            return View(submissionModels);
        }
    }
}