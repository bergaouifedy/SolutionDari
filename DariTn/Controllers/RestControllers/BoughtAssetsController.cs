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
    public class BoughtAssetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BoughtAssets
        public ActionResult Index(int? id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/clients/1/boughtassets").Result;
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/clients/"+id+"/boughtassets").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<BoughtAsset>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<BoughtAsset>());
            }
        }

        // GET: BoughtAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtAsset boughtAsset = db.BoughtAssets.Find(id);
            if (boughtAsset == null)
            {
                return HttpNotFound();
            }
            return View(boughtAsset);
        }

        // GET: BoughtAssets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoughtAssets/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreationDate")] BoughtAsset boughtAsset)
        {
            if (ModelState.IsValid)
            {
                db.BoughtAssets.Add(boughtAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boughtAsset);
        }

        // GET: BoughtAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtAsset boughtAsset = db.BoughtAssets.Find(id);
            if (boughtAsset == null)
            {
                return HttpNotFound();
            }
            return View(boughtAsset);
        }

        // POST: BoughtAssets/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreationDate")] BoughtAsset boughtAsset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boughtAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boughtAsset);
        }

        // GET: BoughtAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtAsset boughtAsset = db.BoughtAssets.Find(id);
            if (boughtAsset == null)
            {
                return HttpNotFound();
            }
            return View(boughtAsset);
        }

        // POST: BoughtAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoughtAsset boughtAsset = db.BoughtAssets.Find(id);
            db.BoughtAssets.Remove(boughtAsset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Insure(int? id)
        {
            return RedirectToAction("Create", "Insurances", new { asset = id });
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
