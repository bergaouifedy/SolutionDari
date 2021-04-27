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
                var aa = response.Content.ReadAsAsync<IEnumerable<AssetAdv>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<AssetAdv>());
            }


        }

        public ActionResult MyAdvs()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getAdvByUser/"+iduser).Result;
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getAdvByUser/4").Result;
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
        public ActionResult Sale()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/AdvTypeAvail/Sale").Result;
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

        public ActionResult Rent()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/AdvTypeAvail/Rent").Result;
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




        public ActionResult Target1()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getTargetAsset/"+iduser).Result;
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

        public ActionResult Target2()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getTargetAsset2/" + iduser).Result;
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


        public ActionResult AddComplaint(int? id)
        {

            return RedirectToAction("Create", "Complaints",new { id=id});
            

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
            return RedirectToAction("MyAdvs");
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
            return RedirectToAction("MyAdvs");

        }

        public ActionResult DeleteAdmin(int? id)
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
            return RedirectToAction("AARequests");

        }

        public ActionResult Accept(int id, AssetAdv aa)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<AssetAdv>("http://localhost:8081/Dari/servlet/acceptAA/" + id, aa).Result;
            return RedirectToAction("AARequests");

        }

        public ActionResult AARequests()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/StatusFalse").Result;
            if (response.IsSuccessStatusCode)
            {
                var aa = response.Content.ReadAsAsync<IEnumerable<AssetAdv>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new AssetAdv());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // LES FONCTIONS GET MTE3 L FILTRE

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {


            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = "";
            if (!string.IsNullOrEmpty(collection["checkBox1"])) {
                string checkBox1 = collection["checkBox1"];
                bool checkBox1B = Convert.ToBoolean(checkBox1);
                string pricemax = collection["maxprice"];
                string pricemin = collection["minprice"];
                
                path = "http://localhost:8081/Dari/servlet/Price/" + pricemin + "/" + pricemax;
                
            }
            Console.Write(path);

            HttpResponseMessage response = httpClient.GetAsync(path).Result;
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

        [HttpPost]
        public ActionResult Search1(string maxprice1,string minprice1)
        {


            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = "http://localhost:8081/Dari/servlet/CategoryAvail/Apartment";
            if (!minprice1.Equals(""))
            {
                 path = "http://localhost:8081/Dari/servlet/Price/" + minprice1 + "/" + maxprice1;
            } else if ((minprice1=="")&&(maxprice1==""))
            {
                 path = "http://localhost:8081/Dari/servlet/AvailableTrue";
            }
             

            

            HttpResponseMessage response = httpClient.GetAsync(path).Result;
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
    }
}
