using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DariTn.Models;
using DariTn.Models.Entities;

namespace DariTn.Controllers.RestControllers
{
    public class GuaranteesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guarantees
        public ActionResult Index()
        {
            return View(db.Guarantees.ToList());
        }

        // GET: Guarantees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantee guarantee = db.Guarantees.Find(id);
            if (guarantee == null)
            {
                return HttpNotFound();
            }
            return View(guarantee);
        }

        // GET: Guarantees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guarantees/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,creationDate,expirationDate,status")] Guarantee guarantee)
        {
            if (ModelState.IsValid)
            {
                db.Guarantees.Add(guarantee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guarantee);
        }

        // GET: Guarantees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantee guarantee = db.Guarantees.Find(id);
            if (guarantee == null)
            {
                return HttpNotFound();
            }
            return View(guarantee);
        }

        // POST: Guarantees/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,creationDate,expirationDate,status")] Guarantee guarantee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guarantee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guarantee);
        }

        // GET: Guarantees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantee guarantee = db.Guarantees.Find(id);
            if (guarantee == null)
            {
                return HttpNotFound();
            }
            return View(guarantee);
        }

        // POST: Guarantees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guarantee guarantee = db.Guarantees.Find(id);
            db.Guarantees.Remove(guarantee);
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
