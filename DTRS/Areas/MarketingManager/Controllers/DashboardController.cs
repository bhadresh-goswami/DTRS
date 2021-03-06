﻿using DTRS.Controllers;
using DTRS.Models;
using DTRS.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DTRS.FilterConfig;

namespace DTRS.Areas.MarketingManager.Controllers
{
    [_AuthenticationFilter]
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
                    //not completed
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

        public ActionResult SubmissionRecWise(string _recname, DateTime? sdate, DateTime? edate)
        {
            ViewBag.Name = "Akash Shah";

            List<SubmissionMaster> list = new List<SubmissionMaster>();
            var original = db.SubmissionMasters.Where(a => a.SBy == _recname).ToList();
            if (sdate != null && edate != null)
            {
                foreach (var item in original)
                {
                    if (item.SDate >= sdate && item.SDate <= edate)
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                list = original;
            }
            return View(list);
        }
    }
}