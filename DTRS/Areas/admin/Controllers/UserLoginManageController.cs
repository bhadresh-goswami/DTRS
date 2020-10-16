using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTRS.Models;

namespace DTRS.Areas.admin.Controllers
{
    public class UserLoginManageController : Controller
    {
        private dbReportingSystemEntities db = new dbReportingSystemEntities();

        // GET: admin/UserLoginManage
        public ActionResult Index()
        {
            var userLoginMasters = db.UserLoginMasters.Include(u => u.RoleMaster);
            return View(userLoginMasters.ToList());
        }

        // GET: admin/UserLoginManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginMaster userLoginMaster = db.UserLoginMasters.Find(id);
            if (userLoginMaster == null)
            {
                return HttpNotFound();
            }
            return View(userLoginMaster);
        }

        // GET: admin/UserLoginManage/Create
        public ActionResult Create()
        {
            ViewBag.UserRole = new SelectList(db.RoleMasters, "RoleId", "RoleTitle");
            return View();
        }

        // POST: admin/UserLoginManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginId,RocketUserName,EmailId,Password,IsEnabled,IsLogin,UserRole")] UserLoginMaster userLoginMaster)
        {
            if (ModelState.IsValid)
            {
                db.UserLoginMasters.Add(userLoginMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserRole = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userLoginMaster.UserRole);
            return View(userLoginMaster);
        }

        // GET: admin/UserLoginManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginMaster userLoginMaster = db.UserLoginMasters.Find(id);
            if (userLoginMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRole = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userLoginMaster.UserRole);
            return View(userLoginMaster);
        }

        // POST: admin/UserLoginManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId,RocketUserName,EmailId,Password,IsEnabled,IsLogin,UserRole")] UserLoginMaster userLoginMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLoginMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserRole = new SelectList(db.RoleMasters, "RoleId", "RoleTitle", userLoginMaster.UserRole);
            return View(userLoginMaster);
        }

        // GET: admin/UserLoginManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLoginMaster userLoginMaster = db.UserLoginMasters.Find(id);
            if (userLoginMaster == null)
            {
                return HttpNotFound();
            }
            return View(userLoginMaster);
        }

        // POST: admin/UserLoginManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLoginMaster userLoginMaster = db.UserLoginMasters.Find(id);
            db.UserLoginMasters.Remove(userLoginMaster);
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
