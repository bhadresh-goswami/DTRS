using DTRS.Controllers;
using DTRS.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DTRS.FilterConfig;

namespace DTRS.Areas.Reports.Controllers
{
    [_AuthenticationFilter]
    public class ViewReportsController : MasterCodeController
    {
        public ViewReportsController()
        {
            db = new Models.dbReportingSystemEntities();
        }
        // GET: Reports/Manager
        public ActionResult Index(DateTime? date)
        {
            DateTime dt = new DateTime();
            if (date != null)
            {
                dt = (DateTime)date;
            }
            string dstr = dt.ToString("yyyy-MM-dd");
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
                    var sub = db.SubmissionMasters.Where(a => a.SBy == recV.UserName && a.SDate==dt).ToList().Count;
                    var intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.SubmissionMaster.SDate==dt).ToList().Count;
                    subT += sub;
                    intT += intv;
                    intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.Status == "Placed" && a.IDate==dt).ToList().Count;
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


            //tls
            List<TeamLeadReportModel> retTLData = new List<TeamLeadReportModel>();
            data = db.UserLoginMasters.Where(a => a.UserRole == 4).ToList();

            foreach (var item in data)
            {
                var rec = db.TeamMasters.Where(a => a.TLName == item.RocketUserName).ToList();
                var recT = rec.Count;
                int subT = 0;
                int intT = 0;
                int poT = 0;
                int canT = 0;
                foreach (var recV in rec)
                {
                    var sub = db.SubmissionMasters.Where(a => a.SBy == recV.UserName && a.SDate == dt).ToList().Count;
                    var intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.IDate == dt).ToList().Count;
                    subT += sub;
                    intT += intv;
                    intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.Status == "Placed" && a.IDate == dt).ToList().Count;
                    poT += intv;
                    var can = db.CandidateMasters.Where(a => a.AssignTo == recV.UserName).ToList().Count;
                    canT += can;
                }

                TeamLeadReportModel m = new TeamLeadReportModel();
                m.Location = item.Location;
                m.ManagerName = rec[0].ManagerName;
                m.totalCandidate = canT;
                m.totalInterview = intT;
                m.totalPO = poT;
                m.totalRecruiter = recT;
                m.totalSubmission = subT;
                m.ID = item.LoginId;
                m.TLName = item.FullName;
                retTLData.Add(m);
            }


            //recruiters
            List<RecruiterReportModel> retRecData = new List<RecruiterReportModel>();
            data = db.UserLoginMasters.Where(a => a.UserRole == 3).ToList();

            foreach (var item in data)
            {
                var rec = db.TeamMasters.Where(a => a.UserName == item.RocketUserName).ToList();
                var recT = rec.Count;
                int subT = 0;
                int intT = 0;
                int poT = 0;
                int canT = 0;
                foreach (var recV in rec)
                {
                    var sub = db.SubmissionMasters.Where(a => a.SBy == recV.UserName && a.SDate == dt).ToList().Count;
                    var intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.IDate == dt).ToList().Count;
                    subT += sub;
                    intT += intv;
                    intv = db.InterviewMasters.Where(a => a.SubmissionMaster.SBy == recV.UserName && a.Status == "Placed" && a.IDate == dt).ToList().Count;
                    poT += intv;
                    var can = db.CandidateMasters.Where(a => a.AssignTo == recV.UserName).ToList().Count;
                    canT += can;
                }

                RecruiterReportModel m = new RecruiterReportModel();
                m.Location = item.Location;
                m.ManagerName = rec[0].ManagerName;
                m.totalCandidate = canT;
                m.totalInterview = intT;
                m.totalPO = poT;
                m.totalRecruiter = recT;
                m.totalSubmission = subT;
                m.ID = item.LoginId;
                m.TLName = rec[0].TLName;
                m.RecruiterName = item.FullName;
                retRecData.Add(m);
            }
            ViewBag.TL = retTLData;
            ViewBag.MG = retData;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["TL"] = retTLData;
            dict["MG"] = retData;
            dict["REC"] = retRecData;

            return View(dict);


        }
    }
}