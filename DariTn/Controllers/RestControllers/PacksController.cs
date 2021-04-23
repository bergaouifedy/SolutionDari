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
    public class PacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Packs
        public ActionResult Index()
        {
            return RedirectToAction("Index", "InsuranceAgencies");
        }

        // GET: Packs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pack pack = db.Packs.Find(id);
            if (pack == null)
            {
                return HttpNotFound();
            }
            return View(pack);
        }

        // GET: Packs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, [Bind(Include = "name,garagePrice,poolPrice,appartementPrice,housePrice,roomPrice,squaremeterPrice,belongingRatio,description")] Pack pack)
        {
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:44362");
                httpClient.PostAsJsonAsync<Pack>("http://localhost:8081/Dari/servlet/insuranceagencies/" + id + "/addpackage", pack).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index", "InsuranceAgencies");
            }

            return View(pack);
        }

        // GET: Packs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/insuranceagencies/packages/" + id + "/get").Result;
            var pack = response.Content.ReadAsAsync<Pack>().Result;
            return View(pack);
        }

        // POST: Packs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id, name,garagePrice,poolPrice,appartementPrice,housePrice,roomPrice,squaremeterPrice,belongingRatio,description")] Pack pack)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.PostAsJsonAsync<Pack>("http://localhost:8081/Dari/servlet/insuranceagencies/packages/" + pack.id + "/modify", pack).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", "InsuranceAgencies");
        }

        // GET: Packs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/insuranceagencies/packages/" + id + "/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", "InsuranceAgencies");
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
