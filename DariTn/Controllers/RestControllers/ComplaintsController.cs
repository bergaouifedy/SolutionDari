using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DariTN.Models.Entities;
using DariTn.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DariTn.Controllers.RestControllers
{
    public class ComplaintsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Complaints
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/MyComplaints").Result;
            if (response.IsSuccessStatusCode)
            {
                //ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result;
                var comp = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result;
                return View(comp);
            }
            else
            {
                ViewBag.result = "error";
                return View(new Complaint());
            }
        }

        // GET: Complaints/ComplaintRequests
        public ActionResult ComplaintRequests()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Complaint/all").Result;
            if (response.IsSuccessStatusCode)
            {
                //ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result;
                var comp = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result;
                return View(comp);
            }
            else
            {
                ViewBag.result = "error";
                return View(new Complaint());
            }
        }

     /*   public ActionResult AddComplaint(int iduser, Complaint comp)
        {
            HttpClient client = new HttpClient();
            String baseAddress = "http://localhost:44362/";
            client.PostAsJsonAsync<Complaint>("http://localhost:8081/Dari/servlet/addComplaint/" + iduser, comp).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Create");
        }*/

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ref,description,status,creationDate")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaint);
        }

        // GET: Complaints/Edit/5
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
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getComplaint/" + id).Result;
            var complaint = tokenResponse.Content.ReadAsAsync<Complaint>().Result;
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Complaint comp)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<Complaint>("http://localhost:8081/Dari/servlet/updateComplaint/" + id+"/"+ comp.description,comp).Result;
            return RedirectToAction("Index");
        }

        // GET: Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/Complaint/delete/" + id).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

     

     /*   // POST: Complaints/Accept/5
        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public ActionResult Accept(int id)
        {
        /*
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.PutAsync("http://localhost:8081/Dari/servlet/acceptComplaint/" + id);
            return RedirectToAction("Index");
        }*/

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
