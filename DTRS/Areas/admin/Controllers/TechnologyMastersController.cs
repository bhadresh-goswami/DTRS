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

    public class TechnologyMastersController : Controller
    {
        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: admin/TechnologyMasters
        public ActionResult Index()
        {
            return View(db.TechnologyMasters.ToList());
        }

        // GET: admin/TechnologyMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnologyMaster technologyMaster = db.TechnologyMasters.Find(id);
            if (technologyMaster == null)
            {
                return HttpNotFound();
            }
            return View(technologyMaster);
        }

        // GET: admin/TechnologyMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/TechnologyMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "TechId,TechName")] TechnologyMaster technologyMaster)
        {
            try
            {
                var tech = db.TechnologyMasters.OrderByDescending(a => a.TechId).ToList();

                int id = 1;
                if (tech.Count > 0)
                {
                    id = tech[0].TechId + 1;
                }
                technologyMaster.TechId = id;
                db.TechnologyMasters.Add(technologyMaster);
                db.SaveChanges();
                TempData["Message"] = "Technology Name Saved!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }


            return RedirectToAction("Index");
        }

        // GET: admin/TechnologyMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnologyMaster technologyMaster = db.TechnologyMasters.Find(id);
            if (technologyMaster == null)
            {
                return HttpNotFound();
            }
            return View(technologyMaster);
        }

        // POST: admin/TechnologyMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TechId,TechName")] TechnologyMaster technologyMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technologyMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(technologyMaster);
        }

        // GET: admin/TechnologyMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnologyMaster technologyMaster = db.TechnologyMasters.Find(id);
            if (technologyMaster == null)
            {
                return HttpNotFound();
            }
            return View(technologyMaster);
        }

        // POST: admin/TechnologyMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechnologyMaster technologyMaster = db.TechnologyMasters.Find(id);
            db.TechnologyMasters.Remove(technologyMaster);
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
