using DTRS.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DTRS.Models;
using System.Web.Security;

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
                Session["name"] = user.RocketUserName;

                //switch (user.UserRole)
                //{
                //    //admin
                //    case 1:
                //        return RedirectToAction("Index", "Dashboard", new { @area = "admin" });
                //    default:
                //        break;
                //}
                var type = Session["type"].ToString();
                string url = "";
                switch (type)
                {

                    case "Admin":
                        url = Url.Action("Index", "Dashboard", new { @area = "admin" });
                        break;
                    case "Recruiter":
                        url = Url.Action("Index", "Dashboard", new { @area = "Recruiter" });
                        break;

                    case "Marketing Manager":
                        url = Url.Action("Index", "Dashboard", new { @area = "MarketingManager" });
                        break;
                    case "Technical Expert":
                        url = Url.Action("Index", "Dashboard", new { @area = "TechnicalExpert" });
                        break;
                    case "Marketing Team Lead":
                        url = Url.Action("Index", "Dashboard", new { @area = "TechnicalLead" });
                        break;

                    case "Technical Expert Lead":
                        url = Url.Action("Index", "Dashboard", new { @area = "TechnicalLead" });
                        break;
                    case "Master Admin":
                        url = Url.Action("Index", "Dashboard", new { @area = "admin" });
                        break;

                    default:
                        break;
                }
                var authTicket = new FormsAuthenticationTicket(
      1,
      user.LoginId.ToString(),  //user id
      DateTime.Now,
      DateTime.Now.AddMinutes(20),  // expiry
      true,  //true to remember
      "", //roles 
      "/"
    );

                //encrypt the ticket and add it to a cookie
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                return Redirect(url);
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

        public ActionResult NotUser()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}