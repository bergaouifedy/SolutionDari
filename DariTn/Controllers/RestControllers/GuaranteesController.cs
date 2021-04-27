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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guarantees
        public ActionResult Index(int? id)
        {
            HttpCookie asset = new HttpCookie("asset");
            asset["id"] = Convert.ToString(id);
            asset.Expires.Add(new TimeSpan(0, 1, 0));
            Response.Cookies.Add(asset);

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
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/credits/clients/"+client+"/get").Result;
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/guarantees/"+id+"/pdf").Result;
            return Redirect("http://localhost:8081/Dari/servlet/guarantees/" + id + "/pdf");
        }

        public ActionResult Validate(int? id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync("http://localhost:8081/Dari/servlet/guarantees/"+id+"/validate", null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index", new { id = Int32.Parse(Request.Cookies["id"].Value) });
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
        public ActionResult Delete(int? id)
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

        // POST: Guarantees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guarantee guarantee = db.Guarantees.Find(id);
            db.Guarantees.Remove(guarantee);
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
