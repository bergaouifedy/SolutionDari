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
    public class ShoppingCartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getpanierByIdUser/3").Result;
            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<ShoppingCart>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<ShoppingCart>());
            }
        }

      

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        

        public int Created( Delivery delivery)
        {
          
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PostAsJsonAsync<Delivery>("http://localhost:8081/Dari/servlet/addDelivery", delivery).Result;
            return delivery.id;
        }

        public Orders AddOrder(String @ref , int idd ,int iduser , float Tprix)
        {
            Orders ord = new Orders();
            HttpClient httpClient2 = new HttpClient();
            httpClient2.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse2 = httpClient2.PostAsJsonAsync<Orders>("http://localhost:8081/Dari/servlet/addOrders/" + @ref + "/" + idd + "/" + iduser + "/" + Tprix, ord).Result;
            return ord;
        }

        public ShoppingCart GetPanier(int id)
        {


            HttpClient httpClient0;
            httpClient0 = new HttpClient();
            httpClient0.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient0.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient0.GetAsync("http://localhost:8081/Dari/servlet/getpanierById/" + id).Result;
            var s = tokenResponse.Content.ReadAsAsync<ShoppingCart>().Result;
            return s;
        }
        // Ajouter Delivery + order+ cartsold
        // POST: ShoppingCarts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstName,lastName,phoneNumber,email,address,postalCode")] Delivery delivery, int idf,int idu,float p)
            {

            int idd = Created(delivery);
            Orders ord = AddOrder("ref"+idf, idd , idu, p);

            CartSold c = new CartSold();
            HttpClient httpClient3 = new HttpClient();
            httpClient3.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse3 = httpClient3.PostAsJsonAsync<CartSold>("http://localhost:8081/Dari/servlet/addCartSold/" + idf + "/" + ord.id, c).Result;

            return RedirectToAction("Index");


        }

    // GET: ShoppingCarts/Delete/5
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
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getpanierById/" + id).Result;
            var s = tokenResponse.Content.ReadAsAsync<ShoppingCart>().Result;

            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/deletefurniturefrompanier/" + id).Result;
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
