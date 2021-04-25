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
using DariTN.Models.Entities;

namespace DariTn.Controllers.RestControllers
{
    public class AssetAdvsController : Controller
    {
        private int iduser = 1; // a remplacer avec id user connecté
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

        public ActionResult TargetedAdv()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getTargetAsset/"+iduser).Result;
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


            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getAssetAdv/" + id).Result;
            var assetAdv = tokenResponse.Content.ReadAsAsync<AssetAdv>().Result;
            return View(assetAdv);
        }

        // GET: AssetAdvs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComplaint()
        {
            Complaint comp = new Complaint();
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
            String baseAddress = "http://localhost:44362/";
            client.PostAsJsonAsync<Complaint>("http://localhost:8081/Dari/servlet/addComplaint/" + iduser, comp).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("AddComplaint");
            }

            return View("AddComplaint");
        }

        // POST: AssetAdvs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetAdv asset)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                String baseAddress = "http://localhost:44362/";
                client.PostAsJsonAsync<AssetAdv>("http://localhost:8081/Dari/servlet/addAssetAdv/"+iduser, asset).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
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


            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getAssetAdv/" + id).Result;
            var assetAdv = tokenResponse.Content.ReadAsAsync<AssetAdv>().Result;

            if (assetAdv == null)
            {
                return HttpNotFound();
            }
            return View(assetAdv);
        }

        // POST: AssetAdvs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AssetAdv assetAdv)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<AssetAdv>("http://localhost:8081/Dari/servlet/updateAssetAdv/" + id, assetAdv).Result;
            return RedirectToAction("Index");
        }

        // GET: AssetAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // SELIM HOUNI TANSECH TZID USER CONNECTE MAYNAJEM YFASAKH KEN LES ANNONCES MTE3OU

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/AssetAdv/delete/" + id).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
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
