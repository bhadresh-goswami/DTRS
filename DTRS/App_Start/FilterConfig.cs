using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

using DTRS.Models;
using DTRS.Models.Login;

namespace DTRS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public class _AuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserId"])))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
                {
                    //Redirecting the user to the Login View of Account Controller  
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                     { "controller", "Login" },
                     { "action", "NotUser" },
                     {"area","" }
                    });
                }
            }
        }

        public class _AuthorizeAttribute : AuthorizeAttribute
        {
            private readonly string[] allowedroles;
            public _AuthorizeAttribute(params string[] roles)
            {
                this.allowedroles = roles;
            }
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                bool authorize = false;
                var userId = Convert.ToString(httpContext.Session["userId"]);
                LoginModel loginModel = new LoginModel();
                string _role = httpContext.Session["type"].ToString();
                if (!string.IsNullOrEmpty(userId))
                {
                    foreach (var role in allowedroles)
                    {
                        if (role == _role) return true;
                    }

                }



                return authorize;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Home" },
                    { "action", "NotUser" }
                   });
            }
        }
    }
}
