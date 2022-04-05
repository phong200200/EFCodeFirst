using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFCodeFirst.DAL;
using EFCodeFirst.Models;

namespace EFCodeFirst.Controllers
{
    public class DummiesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Dummies
        public ActionResult Index()
        {
            var dummies = db.Dummies.Include(d => d.Administrator);
            return View(dummies.ToList());
        }

        // GET: Dummies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dummy dummy = db.Dummies.Find(id);
            if (dummy == null)
            {
                return HttpNotFound();
            }
            return View(dummy);
        }

        // GET: Dummies/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
            return View();
        }

        // POST: Dummies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Dummy dummy)
        {
            if (ModelState.IsValid)
            {
                db.Dummies.Add(dummy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", dummy.InstructorID);
            return View(dummy);
        }

        // GET: Dummies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dummy dummy = db.Dummies.Find(id);
            if (dummy == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", dummy.InstructorID);
            return View(dummy);
        }

        // POST: Dummies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Dummy dummy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dummy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", dummy.InstructorID);
            return View(dummy);
        }

        // GET: Dummies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dummy dummy = db.Dummies.Find(id);
            if (dummy == null)
            {
                return HttpNotFound();
            }
            return View(dummy);
        }

        // POST: Dummies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dummy dummy = db.Dummies.Find(id);
            db.Dummies.Remove(dummy);
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
