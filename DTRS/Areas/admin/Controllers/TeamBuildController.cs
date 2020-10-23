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

    public class TeamBuildController : Controller
    {
        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: admin/TeamBuild
        public ActionResult Index()
        {
            return View(db.TeamMasters.ToList());
        }

        // GET: admin/TeamBuild/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMaster teamMaster = db.TeamMasters.Find(id);
            if (teamMaster == null)
            {
                return HttpNotFound();
            }
            return View(teamMaster);
        }

        // GET: admin/TeamBuild/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/TeamBuild/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "TID,UserName,TLName,ManagerName")] TeamMaster teamMaster)
        {
            try
            {

                db.TeamMasters.Add(teamMaster);
                db.SaveChanges();
                TempData["Message"] = "Team Detail Saved!";
            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        // GET: admin/TeamBuild/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMaster teamMaster = db.TeamMasters.Find(id);
            if (teamMaster == null)
            {
                return HttpNotFound();
            }
            return View(teamMaster);
        }

        // POST: admin/TeamBuild/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TID,UserName,TLName,ManagerName")] TeamMaster teamMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamMaster);
        }

        // GET: admin/TeamBuild/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMaster teamMaster = db.TeamMasters.Find(id);
            if (teamMaster == null)
            {
                return HttpNotFound();
            }
            return View(teamMaster);
        }

        // POST: admin/TeamBuild/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMaster teamMaster = db.TeamMasters.Find(id);
            db.TeamMasters.Remove(teamMaster);
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
