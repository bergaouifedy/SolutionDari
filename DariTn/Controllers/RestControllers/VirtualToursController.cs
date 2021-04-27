using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DariTn.Models;
using DariTn.Models.Entities;

namespace DariTn.Controllers.RestControllers
{
    public class VirtualToursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VirtualTours
        public ActionResult Index(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/VirtualTour/" + id).Result;
            var vt = response.Content.ReadAsAsync<VirtualTour>().Result;

            return View(vt);
        }

        // GET: VirtualTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VirtualTour virtualTour = db.VirtualTours.Find(id);
            if (virtualTour == null)
            {
                return HttpNotFound();
            }
            return View(virtualTour);
        }

        // GET: VirtualTours/Create
        public ActionResult Create()
        {
            ViewBag.id = new SelectList(db.AssetAdvs, "id", "ref");
            return View();
        }

        // POST: VirtualTours/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VirtualTour virtualTour)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.PostAsJsonAsync<VirtualTour>("http://localhost:8081/Dari/servlet/Asset/addVirtualTour", virtualTour).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index","AssetAdvs");
        }

        // GET: VirtualTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VirtualTour virtualTour = db.VirtualTours.Find(id);
            if (virtualTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = new SelectList(db.AssetAdvs, "id", "ref", virtualTour.id);
            return View(virtualTour);
        }

        // POST: VirtualTours/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,link,assetid")] VirtualTour virtualTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(virtualTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id = new SelectList(db.AssetAdvs, "id", "ref", virtualTour.id);
            return View(virtualTour);
        }

        // GET: VirtualTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VirtualTour virtualTour = db.VirtualTours.Find(id);
            if (virtualTour == null)
            {
                return HttpNotFound();
            }
            return View(virtualTour);
        }

        // POST: VirtualTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VirtualTour virtualTour = db.VirtualTours.Find(id);
            db.VirtualTours.Remove(virtualTour);
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
