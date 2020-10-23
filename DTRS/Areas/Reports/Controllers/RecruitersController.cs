using DTRS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.Reports.Controllers
{
    public class RecruitersController : MasterCodeController
    {
        // GET: Reports/Recruiters
        public ActionResult Index()
        {

            return View();
        }
    }
}