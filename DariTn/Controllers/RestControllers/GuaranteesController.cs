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
    public class GuaranteesController : Controller
    {
        public static int a = 0;
        
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Sell(int? asset, int? client)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync("http://localhost:8081/Dari/servlet/clients/"+client+"/buy/"+asset, null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", new { id = a });
        }

        // GET: Guarantees
        public ActionResult Index(int? id)
        {
            a = (int)id;
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/credits/clients/"+client+"/get").Result;
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/assetadvs/"+id+"/guarantees/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Guarantee>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Guarantee>());
            }
        }

        public ActionResult Download(int? id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/guarantees/"+id+"/pdf").Result;
            return Redirect("http://localhost:8081/Dari/servlet/guarantees/" + id + "/pdf");
        }

        public ActionResult Validate(int? id, int? asset)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync("http://localhost:8081/Dari/servlet/guarantees/"+id+"/validate", null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", new { id = asset });
        }

        // GET: Guarantees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantee guarantee = db.Guarantees.Find(id);
            if (guarantee == null)
            {
                return HttpNotFound();
            }
            return View(guarantee);
        }

        // GET: Guarantees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guarantees/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,creationDate,expirationDate,status")] Guarantee guarantee)
        {
            if (ModelState.IsValid)
            {
                db.Guarantees.Add(guarantee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guarantee);
        }

        // GET: Guarantees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guarantee guarantee = db.Guarantees.Find(id);
            if (guarantee == null)
            {
                return HttpNotFound();
            }
            return View(guarantee);
        }

        // POST: Guarantees/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,creationDate,expirationDate,status")] Guarantee guarantee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guarantee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guarantee);
        }

        // GET: Guarantees/Delete/5
        public ActionResult Delete(int? id, int? asset)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/guarantees/"+id+"/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", new { id = asset });
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
