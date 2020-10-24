using DTRS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DTRS.Models;
using DTRS.Models.Submission;
using static DTRS.FilterConfig;
using System.Net.Mail;
using System.Net;

namespace DTRS.Areas.Recruiter.Controllers
{
    [_AuthenticationFilter]
    public class DashboardController : MasterCodeController
    {
        public DashboardController()
        {
            db = new dbReportingSystemEntities();
        }
        // GET: Recruiter/Dashboard
        public ActionResult Index()
        {
            int id = int.Parse(Session["userId"].ToString());
            var user = db.UserLoginMasters.SingleOrDefault(a => a.LoginId == id);
            return View(db.SubmissionMasters.Where(a=>a.SBy == user.RocketUserName));
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(SubmissionMaster model)
        {
            try
            {
                int id = int.Parse(Session["userId"].ToString());
                var user = db.UserLoginMasters.Find(id);
                model.SDate = DateTime.Now.Date;
                model.STime = DateTime.Now.TimeOfDay;
                model.SBy = Session["name"].ToString();
                model.Location = user.Location;
                model.InterviewDate = null;
                model.InterviewFeedBack = "";
                model.InterviewStatus = "Interview Pending";
                model.InterviewTime = null;
                model.AssingedTo = "";
                db.SubmissionMasters.Add(model);
                db.SaveChanges();
                if (model.SId != 0)
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("dashtech.reports@gmail.com");
                    message.To.Add(new MailAddress("brijesh.p@dashtechinc.com"));
                    message.CC.Add("bhadresh@dashtechinc.com");
                    message.CC.Add("kiran@dashtechinc.com");
                    message.CC.Add("nirav@dashtechinc.com");
                    message.CC.Add("akash.s@dashtechinc.com");


                    //message.Bcc.Add("devansh@dashtechinc.com");
                    //message.Bcc.Add("nirav@dashtechinc.com");
                    //message.CC.Add(userAssigned.EmailId);
                    string sub = "RE: New Submission (" + model.CandidateName + ")"; //+ userassignto.UserName.ToUpper();// + "(" + data.CandidateName +")";//"Task Assigned to:" + userassignto.UserName.ToUpper() + " For:" + 
                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    //message.Body = String.Format("Hi {7}<br><br><b>Task Title</b>:{0}<br><b><hr>Details</b>:<br>{1}<br><br><hr>Date:{2} to {3} and Time{4} to {5}<br/><br/>Task Status is {6}", data.Title, data.Details, data.StartDate.ToShortDateString(), data.EndDate.ToShortDateString(), data.StartTime, data.EndTime, status, userassignto.UserName);
                    string strBody = string.Format("Hi Team,<br/>We have new Submission. Details are as blow.<br/><br/>" +
                        "<table border='1'>" +
                        "<tr><td><b>Submission Date</b></td> <td>{0}</td></tr>" +
                        "<tr><td><b>Candaidate Name</b></td> <td>{1}</td></tr>" +
                        "<tr><td><b>Job Title</b></td> <td>{2}</td></tr>" +
                        "<tr><td><b>Rate (per hour)</b></td> <td>${3}</td></tr>" +
                        "<tr><td><b>Client Name</b></td> <td>{4}</td></tr>" +
                        "<tr><td><b>Vendor Name</b></td> <td>{5}</td></tr>" +
                        "<tr><td><b>Email ID</b></td> <td>{6}</td></tr>" +
                        "<tr><td><b>Vendor Company</b></td> <td>{7}</td></tr>" +
                        "<tr><td><b>Submitted By</b></td> <td>{8}</td></tr>" +
                        "</table>"
                        , model.SDate.ToShortDateString(), model.CandidateName, model.Technology, model.Rate, model.ClientName, model.VendorName, model.ContactEmailId, model.VendorCompanyName, model.SBy);
                    message.Body = strBody;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("dashtech.reports@gmail.com", "DashTech@007");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    string cc = "";
                    //foreach (var item in db.CandidateMasters.ToList())
                    //{
                    //    if (data.Details.Contains(item.CandidateName))
                    //    {
                    //        cc += item.MarketingPersonEmailIds;
                    //        break;
                    //    }
                    //}
                    //message.CC.Add(cc);
                    //string msg = String.Format("Task Title:{0}, Details:{1}, Time:{2}", Title, Details,DateTime.Now.TimeOfDay.ToString("hh:mm tt"));

                    //msg = String.Format("Task Title:{0}, Details:{1}", data.Title, data.Details);//, data.CandidateName);
                    //                                                                             //res.Error += "-"+msg;
                    //var client = new RestClient("https://coderwithmustache.com/api/v1/chat.postMessage?oauth_consumer_key=&oauth_token=Deb472PqDIrucDWLZwdWvGpk2G5u-aKS5tvbcl2xVtV&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1595013934&oauth_nonce=omf3AT&oauth_version=1.0&oauth_signature=0DwSoSoqKKUkR%2064X89Hn9Cr948%3D");
                    //var request = new RestRequest(Method.POST);
                    //request.AddHeader("postman-token", "d3b5a59f-06a3-68e3-bcbf-8f71ec36d3f9");
                    //request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("x-user-id", "KzD43WBGrk6D3ebot");
                    //request.AddHeader("x-auth-token", "Deb472PqDIrucDWLZwdWvGpk2G5u-aKS5tvbcl2xVtV");
                    //request.AddHeader("content-type", "application/json");
                    //request.AddParameter("application/json", "{ \"roomid\":\"KzD43WBGrk6D3ebotKzD43WBGrk6D3ebot\", \"channel\": \"@" + userassignto.UserName + "\", \"text\": \" @" + userassignto.UserName + " " + data.Title + "\" }", ParameterType.RequestBody);
                    //IRestResponse response = client.Execute(request);
                    //res.data = "Task Assigned! ";
                    try
                    {
                        smtp.Send(message);
                    }
                    catch (Exception err)
                    {
                        string data = err.Message;
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddInterview(int? sid)
        {
            if (sid == null)
            {
                TempData["Warning"] = "Please selecte the Submission!";
                return RedirectToAction("Index");
            }
            int id = int.Parse(Session["userId"].ToString());
            var user = db.SubmissionMasters.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult AddInterview(int? SId, SubmissionMaster model,string AssingedToOther)
        {

           
            try
            {
                var submission = db.SubmissionMasters.Find(SId);
               
                submission.InterviewDate = model.InterviewDate;
                submission.InterviewFeedBack = "";
                submission.InterviewStatus = "Interview Scheduled";
                submission.InterviewTime = model.InterviewTime;

                var toemailidlist = "";
                if (AssingedToOther != "")
                {
                    submission.AssingedTo = AssingedToOther;
                }
                else
                {
                    submission.AssingedTo = model.AssingedTo;
                    var user = db.UserLoginMasters.SingleOrDefault(a => a.RocketUserName == AssingedToOther);
                    toemailidlist += user.EmailId;
                }
                

            if (sid == null)
            {
                TempData["Warning"] = "Please selecte the Submission!";
                return RedirectToAction("Index");
            }
            try
            {
                int id = int.Parse(Session["userId"].ToString());
                var user = db.SubmissionMasters.Find(id);

                user.InterviewDate = model.InterviewDate;
                user.InterviewFeedBack = "";
                user.InterviewStatus = "Interview Scheduled";
                user.InterviewTime = model.InterviewTime;
                user.AssingedTo = model.AssingedTo;

                db.SaveChanges();
                TempData["Message"] = "Interview Schedule!";
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(SubmissionMaster model)
        {
            try
            {
                var data = db.SubmissionMasters.Find(model.SId);

                //model.SDate = data.SDate;
                //model.STime = data.STime;
                //model.SBy = Session["name"].ToString();
                //data = model;
                //db.SubmissionMasters.Add(model);
                //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                data.ClientName = model.ClientName;
                data.ContactEmailId = model.ContactEmailId;
                data.Rate = model.Rate;
                data.VendorCompanyName = model.VendorCompanyName;
                data.VendorName = model.VendorName;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}