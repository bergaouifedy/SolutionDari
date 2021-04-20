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
    public class AssetAdvsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssetAdvs
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/AvailableTrue").Result;
            if (response.IsSuccessStatusCode)
            {
                //ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result;
                var aa = response.Content.ReadAsAsync<IEnumerable<AssetAdv>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<AssetAdv>());
            }


        }

        // GET: AssetAdvs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetAdv assetAdv = db.AssetAdvs.Find(id);
            if (assetAdv == null)
            {
                return HttpNotFound();
            }
            return View(assetAdv);
        }

        // GET: AssetAdvs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetAdvs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetAdv asset)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                String baseAddress = "http://localhost:44315/";
                client.PostAsJsonAsync<AssetAdv>("http://localhost:8081/Dari/servlet/addAssetAdv/1", asset).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: AssetAdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetAdv assetAdv = db.AssetAdvs.Find(id);
            if (assetAdv == null)
            {
                return HttpNotFound();
            }
            return View(assetAdv);
        }

        // POST: AssetAdvs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ref,description,type,addedDate,price,surface,street,city,state,postalCode,nbrRooms,nbrComplaints,nbrFloor,floor,nbrBathrooms,furnished,garage,parking,pool,category,availability,status,capacity")] AssetAdv assetAdv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetAdv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetAdv);
        }

        // GET: AssetAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetAdv assetAdv = db.AssetAdvs.Find(id);
            if (assetAdv == null)
            {
                return HttpNotFound();
            }
            return View(assetAdv);
        }

        // POST: AssetAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetAdv assetAdv = db.AssetAdvs.Find(id);
            db.AssetAdvs.Remove(assetAdv);
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
