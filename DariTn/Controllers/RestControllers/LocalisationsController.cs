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
    public class LocalisationsController : Controller
    {
        private int iduser = 1;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Localisation
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/Localisation/" + id).Result;
            var localisation = response.Content.ReadAsAsync<Localisation>().Result;
            
            return View(localisation);
        }
        public ActionResult MyAdvs()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getAdvByUser/" + iduser).Result;
            if (response.IsSuccessStatusCode)
            {
                var aa = response.Content.ReadAsAsync<IEnumerable<AssetAdv>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<AssetAdv>());
            }
        }

        // GET: Localisation
        public ActionResult AllLocations()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/Localisation/All").Result;
            var localisation = response.Content.ReadAsAsync<Localisation>().Result;

            return View(localisation);
        }

        // GET: Localisations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/Localisation/" + id).Result;
            var localisation = response.Content.ReadAsAsync<Localisation>().Result;
            return View(localisation);
        }

        // GET: Localisations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localisations/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Localisation localisation)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44315/");
            httpClient.PostAsJsonAsync<Localisation>("http://localhost:8081/Dari/servlet/Asset/addLocalisation/"+id, localisation).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("MyAdvs");
        }

        // GET: Localisations/Edit/5
       /* public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localisation localisation = db.Localisations.Find(id);
            if (localisation == null)
            {
                return HttpNotFound();
            }
            return View(localisation);
        }*/

        // POST: Localisations/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,latitude,longtitude")] Localisation localisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localisation);
        }

        // GET: Localisations/Delete/5
      /*  public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localisation localisation = db.Localisations.Find(id);
            if (localisation == null)
            {
                return HttpNotFound();
            }
            return View(localisation);
        }
      
        // POST: Localisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localisation localisation = db.Localisations.Find(id);
            db.Localisations.Remove(localisation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      */
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
