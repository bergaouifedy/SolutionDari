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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersByIdUser/3").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<Orders>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<Orders>());
            }
        }

        public ActionResult OrdersNoAccepted()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersNoTraitées").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<Orders>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<Orders>());
            }
        }

        public ActionResult OrdersW()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrderWaiting").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<Orders>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<Orders>());
            }
        }

        public ActionResult OrdersInProg()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrderInprogress").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<Orders>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<Orders>());
            }
        }

        public ActionResult OrdersF()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrderFinished").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<Orders>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<Orders>());
            }
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersById/" + id).Result;
            var ord = tokenResponse.Content.ReadAsAsync<Orders>().Result;

            if (ord == null)
            {
                return HttpNotFound();
            }
            return View(ord);
        }
        public ActionResult DetailsAdmin(int? id)
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersById/" + id).Result;
            var ord = tokenResponse.Content.ReadAsAsync<Orders>().Result;

            if (ord == null)
            {
                return HttpNotFound();
            }
            return View(ord);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Orders/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,reff,totalPrice,deliveryStatus,paymentStatus,adminStatus,deliveryid,userid")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }

            
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersById/" + id).Result;
            var ord = tokenResponse.Content.ReadAsAsync<Orders>().Result;

            HttpClient httpClient11;
            httpClient11 = new HttpClient();
            httpClient11.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient11.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse11 = httpClient11.GetAsync("http://localhost:8081/Dari/servlet/getdbyId/" + ord.delivery.id).Result;
            var d = tokenResponse11.Content.ReadAsAsync<Delivery>().Result;
            return View(ord);
        }

        // POST: Orders/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Delivery d,int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<Delivery>("http://localhost:8081/Dari/servlet/updateDelivery/" + id, d).Result;
            return RedirectToAction("Index");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getOrdersById/" + id).Result;
            var ord = tokenResponse.Content.ReadAsAsync<Orders>().Result;

            if (ord == null)
            {
                return HttpNotFound();
            }
            return View(ord);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/deleteOrders/" + id).Result;
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/deleteOrders/" + id).Result;
            return RedirectToAction("OrdersNoAccepted");
        }

        public ActionResult Accept(int id, Orders f)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/Dari/servlet/acceptOrders/" + id, f).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("OrdersNoAccepted");
        }

        public ActionResult UpdateInProg(int id, Orders f)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/Dari/servlet/LivrerOrders/" + id, f).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("OrdersW");
        }

        public ActionResult UpdateFinished(int id, Orders f)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PutAsJsonAsync("http://localhost:8081/Dari/servlet/LivrerOrders/" + id, f).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("OrdersInProg");
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
