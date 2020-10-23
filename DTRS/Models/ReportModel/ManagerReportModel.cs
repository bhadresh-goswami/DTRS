using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models.ReportModel
{
    public class ManagerReportModel
    {
        public int ID { get; set; }
        public int totalRecruiter { get; set; }
        public int totalSubmission { get; set; }
        public int totalInterview { get; set; }
        public int totalPO { get; set; }
        public int totalCandidate { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }
        public List<TeamLeadReportModel> tlReport { get; set; }
    }

    public class TeamLeadReportModel
    {
        public int ID { get; set; }
        public int totalRecruiter { get; set; }
        public int totalSubmission { get; set; }
        public int totalInterview { get; set; }
        public int totalPO { get; set; }
        public int totalCandidate { get; set; }
        public string Location { get; set; }
        public string TLName { get; set; }
        public string ManagerName { get; set; }

    }

    public class RecruiterReportModel
    {
        public int ID { get; set; }
        public int totalRecruiter { get; set; }
        public int totalSubmission { get; set; }
        public int totalInterview { get; set; }
        public int totalPO { get; set; }
        public int totalCandidate { get; set; }
        public string Location { get; set; }
        public string RecruiterName { get; set; }
        public string TLName { get; set; }
        public string ManagerName { get; set; }
    }
}