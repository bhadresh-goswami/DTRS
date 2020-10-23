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
    public class RoleManageController : Controller
    {
        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: admin/RoleManage
        public ActionResult Index()
        {
            return View(db.RoleMasters.ToList());
        }

        // GET: admin/RoleManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleMaster roleMaster = db.RoleMasters.Find(id);
            if (roleMaster == null)
            {
                return HttpNotFound();
            }
            return View(roleMaster);
        }

        // GET: admin/RoleManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/RoleManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "RoleId,RoleTitle,IsEnabled")] RoleMaster roleMaster)
        {
            try
            {
                //int id = db.RoleMasters.Last().RoleId + 1;
                var role = db.RoleMasters.OrderByDescending(a=>a.RoleId).ToList();
                roleMaster.RoleId = role[0].RoleId + 1;
                db.RoleMasters.Add(roleMaster);
                db.SaveChanges();
                TempData["Message"] = "Role Title Saved!";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        // GET: admin/RoleManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleMaster roleMaster = db.RoleMasters.Find(id);
            if (roleMaster == null)
            {
                return HttpNotFound();
            }
            return View(roleMaster);
        }

        // POST: admin/RoleManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,RoleTitle,IsEnabled")] RoleMaster roleMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleMaster);
        }

        // GET: admin/RoleManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleMaster roleMaster = db.RoleMasters.Find(id);
            if (roleMaster == null)
            {
                return HttpNotFound();
            }
            return View(roleMaster);
        }

        // POST: admin/RoleManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoleMaster roleMaster = db.RoleMasters.Find(id);
            db.RoleMasters.Remove(roleMaster);
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
