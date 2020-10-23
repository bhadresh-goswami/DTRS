using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using System.Data;

using DTRS.Models;
using static DTRS.FilterConfig;

namespace DTRS.Controllers
{
    public class MasterCodeController : Controller
    {
        protected SqlConnection conn;
        protected DataTable dt;
        protected DataSet ds;
        protected SqlDataAdapter adp;
        protected SqlCommand cmd;

        protected dbReportingSystemEntities db;
        public MasterCodeController()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\DashProject\DTRS\DTRS\DTRS\App_Data\dbReportingSystem.mdf;Integrated Security=True");
        }
    }
}