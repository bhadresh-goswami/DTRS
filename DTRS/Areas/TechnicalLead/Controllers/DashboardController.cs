using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.TechnicalLead.Controllers
{
    public class DashboardController : Controller
    {
        // GET: TechnicalLead/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}