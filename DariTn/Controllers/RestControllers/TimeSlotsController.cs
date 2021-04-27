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
    public class TimeSlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeSlots
        public ActionResult Index(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/RDVTS/FinByAsset/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var aa = response.Content.ReadAsAsync<IEnumerable<TimeSlots>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<TimeSlots>());
            }
        }

        // GET: TimeSlots
        public ActionResult Index2(int? id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/RDVTS/FinByAsset/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var aa = response.Content.ReadAsAsync<IEnumerable<TimeSlots>>().Result;
                return View(aa);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<TimeSlots>());
            }
        }

        // GET: TimeSlots/Details/5
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
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/RDVTS/" + id).Result;
            var ts = tokenResponse.Content.ReadAsAsync<TimeSlots>().Result;
            return View(ts);
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
        public ActionResult Create(int idasset,TimeSlots timeSlots)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                String baseAddress = "http://localhost:44362/";
                client.PostAsJsonAsync<TimeSlots>("http://localhost:8081/Dari/servlet/Asset/addRdvts/" +idasset, timeSlots).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index2/"+idasset);
            }

            return View(timeSlots);
        }

        // GET: TimeSlots/Edit/5
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
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Asset/RDVTS/" + id).Result;
            var timeSlots = tokenResponse.Content.ReadAsAsync<TimeSlots>().Result;
            if (timeSlots == null)
            {
                return HttpNotFound();
            }
            return View(timeSlots);
        }

        // POST: TimeSlots/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DateTime date, TimeSpan hdebut, string title, TimeSpan hfin, TimeSlots timeSlots)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<TimeSlots>("http://localhost:8081/Dari/servlet/Asset/RDVTS/"+id+"/Modify/"+date+"/"+title+"/"+hdebut+"/"+hfin , timeSlots).Result;
            return RedirectToAction("Index2/"+timeSlots.idasset);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/Asset/RDVTS/delete/" + id ).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("MyAdvs","AssetAdvs");
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
