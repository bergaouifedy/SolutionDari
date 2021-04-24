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
    public class FurnitureAdvsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FurnitureAdvs
        public ActionResult Index()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getFurnitureAdvDispo").Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;

                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }


        // GET: FurnitureAdvs
        public ActionResult Index2()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getFurnitureAdvByIdUser/3").Result;


            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }

        // GET: FurnitureAdvs/Details/5
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
                HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Findf/" + id).Result;
                var furnitureAdv = tokenResponse.Content.ReadAsAsync<FurnitureAdv>().Result;
                return View(furnitureAdv);

            
        }
        // GET: FurnitureAdvs/Details2/5
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Findf/" + id).Result;
            var furnitureAdv = tokenResponse.Content.ReadAsAsync<FurnitureAdv>().Result;
            return View(furnitureAdv);


        }


        // GET: FurnitureAdvs/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.Users, "id", "firstName");
            return View();
        }

        // POST: FurnitureAdvs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userid,ref,name,description,type,addedDate,price,availability,status,image")] FurnitureAdv furnitureAdv)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PostAsJsonAsync<FurnitureAdv>("http://localhost:8081/Dari/servlet/addFurnitureAdv", furnitureAdv).Result;
            return RedirectToAction("Index2");
        }

        // GET: FurnitureAdvs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // FurnitureAdv furnitureAdv = db.FurnitureAdvs.Find(id);
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Findf/" + id).Result;
            var furnitureAdv = tokenResponse.Content.ReadAsAsync<FurnitureAdv>().Result;

            if (furnitureAdv == null)
            {
                return HttpNotFound();
            }
            return View(furnitureAdv);
        }

        // POST: FurnitureAdvs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,description,type,price,image")] FurnitureAdv furnitureAdv,int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<FurnitureAdv>("http://localhost:8081/Dari/servlet/updateFurnitureAdv/" + id, furnitureAdv).Result;
            return RedirectToAction("Index2");


        }

        // GET: FurnitureAdvs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // FurnitureAdv furnitureAdv = db.FurnitureAdvs.Find(id);
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Findf/" + id).Result;
            var furnitureAdv = tokenResponse.Content.ReadAsAsync<FurnitureAdv>().Result;

            if (furnitureAdv == null)
            {
                return HttpNotFound();
            }
            return View(furnitureAdv);
        }

        // POST: FurnitureAdvs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            HttpResponseMessage tokenResponse = httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/deleteFurnitureAdv/" + id).Result;
            return RedirectToAction("Index2");
        }

        
        public ActionResult Index5(String name)
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/FindByName/"+ name).Result;


            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }

        // GET: FurnitureAdvs
        public ActionResult GetByType(String type)
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/FindByType/" + type).Result;


            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }

        public ActionResult Index3()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/FindByPriceAsc").Result;


            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }
        public ActionResult Index4()
        {
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/FindByPriceDesc").Result;


            if (tokenResponse.IsSuccessStatusCode)
            {
                var f = tokenResponse.Content.ReadAsAsync<IEnumerable<FurnitureAdv>>().Result;
                return View(f);
            }
            else
            {
                return View(new List<FurnitureAdv>());
            }
        }


        public ActionResult AddFurnitureToShoppingCart([Bind(Include = "id,totalPrice,userid,furnitureid")] ShoppingCart shoppingCart, int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44363/api/");
            var APIResponse = httpClient.PostAsJsonAsync<ShoppingCart>("http://localhost:8081/Dari/servlet/addfurnitureaupanier/3/" + id, shoppingCart).Result;
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
