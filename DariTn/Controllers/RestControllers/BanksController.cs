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
    public class BanksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Banks
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/banks/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Bank>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Bank>());
            }
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banks/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Bank bank)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.PostAsJsonAsync<Bank>("http://localhost:8081/Dari/servlet/banks/addbank", bank).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/banks/" + id + "/get").Result;
            var bank = response.Content.ReadAsAsync<Bank>().Result;
            return View(bank);
        }

        // POST: Banks/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Bank bank)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.PostAsJsonAsync<Bank>("http://localhost:8081/Dari/servlet/banks/" + bank.id + "/modify", bank).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        // GET: Banks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/banks/" + id + "/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult AddFormula(int? id)
        {
            return RedirectToAction("Create", "CreditFormulas", new { id = id });
        }

        public ActionResult EditFormula(int? id)
        {
            return RedirectToAction("Edit", "CreditFormulas", new { id = id });
        }

        public ActionResult DeleteFormula(int? id)
        {
            return RedirectToAction("Delete", "CreditFormulas", new { id = id });
        }

        [HttpGet]
        public ActionResult ListCreditFormulas()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/banks/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var Banks = response.Content.ReadAsAsync<List<Bank>>().Result;
                List<SelectListItem> formulas = new List<SelectListItem>();

                for (int i = 0; i < Banks.Count; i++)
                {
                    for (int j = 0; j < Banks.ElementAt(i).CreditFormulas.Count; j++)
                    {
                        formulas.Add(new SelectListItem
                        {
                            Value = Banks.ElementAt(i).CreditFormulas.ElementAt(j).id.ToString(),
                            Text = Banks.ElementAt(i).CreditFormulas.ElementAt(j).ToString()
                        });
                    }
                }

                var countrytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select formula ---"
                };

                formulas.Insert(0, countrytip);
                SelectList liste = new SelectList(formulas, "Value", "Text");

                var credit = new CreditNewViewModel()
                {
                    Formulas = liste
                };

                return View(credit);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Bank>());
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
    }
}
