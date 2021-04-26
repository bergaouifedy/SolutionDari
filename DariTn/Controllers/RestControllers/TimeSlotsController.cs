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
    public class TimeSlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeSlots
        public ActionResult Index()
        {
            var timeSlots = db.TimeSlots.Include(t => t.asset);
            return View(timeSlots.ToList());
        }

        // GET: TimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // GET: TimeSlots/Create
        public ActionResult Create()
        {
            ViewBag.idasset = new SelectList(db.AssetAdvs, "id", "ref");
            return View();
        }

        // POST: TimeSlots/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,hdebut,hfin,title,idasset")] TimeSlots timeSlots)
        {
            if (ModelState.IsValid)
            {
                db.TimeSlots.Add(timeSlots);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idasset = new SelectList(db.AssetAdvs, "id", "ref", timeSlots.idasset);
            return View(timeSlots);
        }

        // GET: TimeSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            ViewBag.idasset = new SelectList(db.AssetAdvs, "id", "ref", timeSlots.idasset);
            return View(timeSlots);
        }

        // POST: TimeSlots/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,hdebut,hfin,title,idasset")] TimeSlots timeSlots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSlots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idasset = new SelectList(db.AssetAdvs, "id", "ref", timeSlots.idasset);
            return View(timeSlots);
        }

        // GET: TimeSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // POST: TimeSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlots timeSlots = db.TimeSlots.Find(id);
            db.TimeSlots.Remove(timeSlots);
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
