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
    public class InsurancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Insurances
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/clients/"+client+"/insurances").Result;
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/clients/1/insurances").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Insurance>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Insurance>());
            }
        }

        // GET: Insurances/Create
        public ActionResult Create()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/insuranceagencies/get").Result;
            if (response.IsSuccessStatusCode)
            {
                List<InsuranceAgency> Agencies = response.Content.ReadAsAsync<List<InsuranceAgency>>().Result;
                List<SelectListItem> packs = new List<SelectListItem>();

                for (int i = 0; i < Agencies.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < Agencies.ElementAt(i).packs.Count; j++)
                    {
                        packs.Add(new SelectListItem
                        {
                            Value = Agencies.ElementAt(i).packs.ElementAt(j).id.ToString(),
                            Text = Agencies.ElementAt(i).packs.ElementAt(j).afficher()
                        });
                    }
                }

                var countrytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select pack ---"
                };

                packs.Insert(0, countrytip);
                SelectList liste = new SelectList(packs, "Value", "Text");

                var insurance = new InsuranceNewViewModel()
                {
                    Packs = liste
                };

                return View(insurance);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Insurance>());
            }
        }

        // POST: Insurances/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? client, int? asset, [Bind(Include = "SelectedPackId")] InsuranceNewViewModel ins)
        {
            if (ModelState.IsValid)
            {
                Insurance insurance = new Insurance();
                Pack CF = new Pack();
                CF.id = Int32.Parse(ins.SelectedPackId);
                insurance.Pack = CF;
                HttpClient httpClient = new HttpClient();
                httpClient.PostAsJsonAsync<Pack>("http://localhost:8081/Dari/servlet/client/1/boughtassets/" + asset + "/addinsurance", CF).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index", new { client = client });
            }

            return View(ins);
        }

        // GET: Insurances/Edit/5
        public ActionResult Edit(int? id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/insuranceagencies/get").Result;
            if (response.IsSuccessStatusCode)
            {
                List<InsuranceAgency> Agencies = response.Content.ReadAsAsync<List<InsuranceAgency>>().Result;
                List<SelectListItem> packs = new List<SelectListItem>();

                for (int i = 0; i < Agencies.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < Agencies.ElementAt(i).packs.Count; j++)
                    {
                        packs.Add(new SelectListItem
                        {
                            Value = Agencies.ElementAt(i).packs.ElementAt(j).id.ToString(),
                            Text = Agencies.ElementAt(i).packs.ElementAt(j).afficher()
                        });
                    }
                }

                var countrytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select pack ---"
                };

                packs.Insert(0, countrytip);
                SelectList liste = new SelectList(packs, "Value", "Text");

                var insurance = new InsuranceEditViewModel()
                {
                    Packs = liste,
                    Id= (int)id
                };

                return View(insurance);
            }
            else
                return HttpNotFound();
        }

        // POST: Insurances/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? client, [Bind(Include = "Id, SelectedPackId")] InsuranceEditViewModel ins)
        {
            if (ModelState.IsValid)
            {
                Insurance insurance = new Insurance();
                insurance.Id = ins.Id;
                Pack CF = new Pack();
                CF.id = Int32.Parse(ins.SelectedPackId);
                insurance.Pack = CF;
                HttpClient httpClient = new HttpClient();
                httpClient.PostAsJsonAsync<Pack>("http://localhost:8081/Dari/servlet/insurances/" + insurance.Id + "/modify", CF).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index", new { client = client });
            }
            return View(ins);
        }

        // GET: Insurances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/insurances/" + id + "/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
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
