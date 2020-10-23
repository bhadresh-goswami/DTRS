using DTRS.Controllers;
using DTRS.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.Reports.Controllers
{
    public class ManagerController : MasterCodeController
    {
        public ManagerController()
        {
            db = new Models.dbReportingSystemEntities();
        }
        // GET: Reports/Manager
        public ActionResult Index()
        {
            List<ManagerReportModel> retData = new List<ManagerReportModel>();
            var data = db.UserLoginMasters.Where(a => a.UserRole == 2).ToList();
            
            foreach (var item in data)
            {
                var rec = db.TeamMasters.Where(a => a.ManagerName == item.RocketUserName).ToList();
                var recT = rec.Count;
                int subT = 0;
                int intT = 0;
                int poT = 0;
                int canT = 0;
                foreach (var recV in rec)
                {
                    var sub = db.SubmissionMasters.Where(a => a.SBy == recV.UserName).ToList().Count;
                    var intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName).ToList().Count;
                    subT += sub;
                    intT += intv;
                    intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.Status == "Placed").ToList().Count;
                    poT += intv;
                    var can = db.CandidateMasters.Where(a => a.AssignTo == recV.UserName).ToList().Count;
                    canT += can;
                }

                ManagerReportModel m = new ManagerReportModel();
                m.Location = item.Location;
                m.ManagerName = item.FullName;
                m.totalCandidate = canT;
                m.totalInterview = intT;
                m.totalPO = poT;
                m.totalRecruiter = recT;
                m.totalSubmission = subT;
                m.ID = item.LoginId;
                retData.Add(m);
            }
            return View(retData);
        }
    }
}