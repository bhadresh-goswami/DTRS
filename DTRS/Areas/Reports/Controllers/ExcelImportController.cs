using DTRS.Models;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.Reports.Controllers
{
    public class ExcelImportController : Controller
    {

        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: Reports/ExcelImport
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>  
        /// This function is used to download excel format.  
        /// </summary>  
        /// <param name="Path"></param>  
        /// <returns>file</returns>  
        public FileResult DownloadExcel()
        {
            string path = "/Doc/Candidate.xlsx";
            return File(path, "application/vnd.ms-excel", "Candidate.xlsx");
        }

        [HttpPost]
        public JsonResult UploadExcel(CandidateMaster candidates, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var candidatesExcel = from a in excelFile.Worksheet<CandidateMaster>(sheetName) select a;

                    foreach (var a in candidatesExcel)
                    {
                        try
                        {
                            if (a.CandidateId != 0 && a.CandidateName != "")
                            {
                                CandidateMaster CAN = new CandidateMaster();
                                CAN.CandidateId = a.CandidateId;
                                CAN.CandidateName = a.CandidateName;
                                CAN.CandidateEmailId = a.CandidateEmailId;
                                CAN.MarketingEmailId = a.MarketingEmailId;
                                CAN.ContactNumber = a.ContactNumber;
                                CAN.MarketingNumver = a.MarketingNumver;
                                CAN.InsertBy = a.InsertBy;
                                CAN.Technology = a.Technology;
                                CAN.AssignTo = a.AssignTo;
                                CAN.VisaStatus = a.VisaStatus;



                                db.CandidateMasters.Add(CAN);

                                db.SaveChanges();



                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.CandidateId == 0 ) data.Add("<li> ID is required</li>");
                                if (a.CandidateName == null) data.Add("<li> NAME is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}