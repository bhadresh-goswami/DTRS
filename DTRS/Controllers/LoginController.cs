using DTRS.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DTRS.Models;

namespace DTRS.Controllers
{
    public class LoginController : MasterCodeController
    {
        public LoginController()
        {
            db = new dbReportingSystemEntities();
        }

        // GET: Login
        public ActionResult Index()
        {
            ViewBag.title = "Login";
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            try
            {
                UserLoginMaster user = db.UserLoginMasters.SingleOrDefault(a => a.EmailId == model.EmailId && a.Password == model.Password);
                if (user == null)
                {
                    TempData["Warning"] = "Email Address or Password is wrong!";
                    return View();
                }
                Session["type"] = user.RoleMaster.RoleTitle;
                Session["userId"] = user.LoginId;

                switch (user.UserRole)
                {
                    //admin
                    case 1:
                        return RedirectToAction("Index", "Dashboard", new { @area = "admin" });
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Server Side Error Please Try Again or Contact to Admin!";
            }
            return View();
        }
        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoverPassword(string EmailId)
        {
            return View();
        }
    }
}