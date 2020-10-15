using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.MarketingManager.Controllers
{
    public class CandidateManageController : Controller
    {
        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: MarketingManager/CandidateManage
        public ActionResult Index()
        {
            return View(db.CandidateMasters.ToList());
        }

        // GET: MarketingManager/CandidateManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // GET: MarketingManager/CandidateManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketingManager/CandidateManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CandidateId,CandidateName,CandidateEmailId,MarketingEmailId,ContactNumber,MarketingNumver,InsertBy,Technology")] CandidateMaster candidateMaster)
        {
            try
            {
                var users = db.CandidateMasters.OrderByDescending(a => a.CandidateId).ToList();
                candidateMaster.CandidateId = users[0].CandidateId + 1;
                candidateMaster.InsertBy = "";
                db.CandidateMasters.Add(candidateMaster);
                db.SaveChanges();

                TempData["Message"] = "Details Saved!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Server side Error, Please contact to admin!" + ex.Message;
            }
            return RedirectToAction("Index", "CandidateManage", new { @area = "MarketingManager" });
        }

        // GET: MarketingManager/CandidateManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // POST: MarketingManager/CandidateManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateId,CandidateName,CandidateEmailId,MarketingEmailId,ContactNumber,MarketingNumver,InsertBy,Technology")] CandidateMaster candidateMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidateMaster);
        }

        // GET: MarketingManager/CandidateManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // POST: MarketingManager/CandidateManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            db.CandidateMasters.Remove(candidateMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
