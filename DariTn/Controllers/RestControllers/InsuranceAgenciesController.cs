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
    public class InsuranceAgenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InsuranceAgencies
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/insuranceagencies/get").Result;

            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<InsuranceAgency>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<InsuranceAgency>());
            }
        }

        // GET: InsuranceAgencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceAgencies/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] InsuranceAgency insuranceAgency)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.PostAsJsonAsync<InsuranceAgency>("http://localhost:8081/Dari/servlet/insuranceagencies/addagency", insuranceAgency).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: InsuranceAgencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/insuranceagencies/" + id + "/get").Result;
            var insuranceAgency = response.Content.ReadAsAsync<InsuranceAgency>().Result;
            return View(insuranceAgency);
        }

        // POST: InsuranceAgencies/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] InsuranceAgency insuranceAgency)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.PostAsJsonAsync<InsuranceAgency>("http://localhost:8081/Dari/servlet/insuranceagencies/" + insuranceAgency.id + "/modify", insuranceAgency).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: InsuranceAgencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/insuranceagencies/" + id + "/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult AddPack(int? id)
        {
            return RedirectToAction("Create", "Packs", new { id = id });
        }

        public ActionResult EditPack(int? id)
        {
            return RedirectToAction("Edit", "Packs", new { id = id });
        }

        public ActionResult DeletePack(int? id)
        {
            return RedirectToAction("Delete", "Packs", new { id = id });
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
