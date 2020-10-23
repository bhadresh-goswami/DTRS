using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;
using static DTRS.FilterConfig;

namespace DTRS.Areas.admin.Controllers
{
    [_AuthenticationFilter]
    public class JobPortalManageController : Controller
    {


        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: admin/JobPortalManage
        public ActionResult Index()
        {
            return View(db.JobPortalMasters.ToList());
        }

        // GET: admin/JobPortalManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            if (jobPortalMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobPortalMaster);
        }

        // GET: admin/JobPortalManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/JobPortalManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "JPId,JobPortalTitle")] JobPortalMaster jobPortalMaster)
        {
            try
            {
                db.JobPortalMasters.Add(jobPortalMaster);
                db.SaveChanges();
                TempData["Message"] = "Portal Name Saved!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException;
            }
            return RedirectToAction("Index");

        }

        // GET: admin/JobPortalManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            if (jobPortalMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobPortalMaster);
        }

        // POST: admin/JobPortalManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "JPId,JobPortalTitle")] JobPortalMaster jobPortalMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPortalMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobPortalMaster);
        }

        // GET: admin/JobPortalManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            if (jobPortalMaster == null)
            {
                return HttpNotFound();
            }
            return View(jobPortalMaster);
        }

        // POST: admin/JobPortalManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPortalMaster jobPortalMaster = db.JobPortalMasters.Find(id);
            db.JobPortalMasters.Remove(jobPortalMaster);
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
