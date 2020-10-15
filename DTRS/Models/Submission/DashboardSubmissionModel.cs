using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models.Submission
{
    public class DashboardSubmissionModel
    {
        public string Name { get; set; }
        public int TSCount { get; set; }
        public int TICount { get; set; }
        public int TPOCount { get; set; }
        public int TCCount { get; set; }
    }
}