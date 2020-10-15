using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.MarketingManager.Controllers
{
    public class DashboardController : Controller
    {
        // GET: MarketingManager/Dashboard
        public ActionResult Index()
        {
            ViewBag.Recruiters = null;
            return View();
        }
    }
}