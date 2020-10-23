using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DTRS.FilterConfig;

namespace DTRS.Areas.admin.Controllers
{
    [_AuthenticationFilter]
    public class DashboardController : Controller
    {
        // GET: admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}