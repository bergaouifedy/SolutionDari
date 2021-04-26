using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DariTn.Models;
using DariTn.Models.Entities;
using Newtonsoft.Json;

namespace DariTn.Controllers.RestControllers
{
    public class CreditsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Credits
        public ActionResult Index(/*int? client*/)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/credits/clients/"+client+"/get").Result;
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/credits/clients/1/get").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Credit>>().Result;
                return View(list);
            }
            else
            {
                ViewBag.result = "error";
                return View(new List<Bank>());
            }
        }

        // GET: Credits/Create
        public ActionResult Create()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/banks/get").Result;
            if (response.IsSuccessStatusCode)
            {
                List<Bank> Banks = response.Content.ReadAsAsync<List<Bank>>().Result;
                List<SelectListItem> formulas = new List<SelectListItem>();
 
                for (int i = 0; i < Banks.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < Banks.ElementAt(i).CreditFormulas.Count; j++)
                    {
                        formulas.Add(new SelectListItem
                        {
                            Value = Banks.ElementAt(i).CreditFormulas.ElementAt(j).id.ToString(),
                            Text = Banks.ElementAt(i).CreditFormulas.ElementAt(j).afficher()
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

        // POST: Credits/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? client, [Bind(Include = "initialamount, SelectedFormulaId")] CreditNewViewModel c)
        {
            if (ModelState.IsValid)
            {
                //httpClient.PostAsJsonAsync<Credit>("http://localhost:8081/Dari/servlet/clients/"+client+"/credits/addcredit", credit).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());

                Credit credit = new Credit();
                credit.initialamount = c.InitialAmount;
                CreditFormula CF = new CreditFormula();
                CF.id = Int32.Parse(c.SelectedFormulaId);
                credit.creditformula = CF;
                HttpClient httpClient = new HttpClient();
                httpClient.PostAsJsonAsync<Credit>("http://localhost:8081/Dari/servlet/clients/1/credits/addcredit", credit).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index", new { client = client });
            }

            return View(c);
        }

        // GET: Credits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/banks/get").Result;

            if (response.IsSuccessStatusCode)
            {
                List<Bank> Banks = response.Content.ReadAsAsync<List<Bank>>().Result;
                List<SelectListItem> formulas = new List<SelectListItem>();

                for (int i = 0; i < Banks.Count; i++)
                {
                    int j = 0;
                    for (j = 0; j < Banks.ElementAt(i).CreditFormulas.Count; j++)
                    {
                        formulas.Add(new SelectListItem
                        {
                            Value = Banks.ElementAt(i).CreditFormulas.ElementAt(j).id.ToString(),
                            Text = Banks.ElementAt(i).CreditFormulas.ElementAt(j).afficher()
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

                var credit = new CreditEditViewModel()
                {
                    Formulas = liste,
                    Id= (int)id
                };

                return View(credit);
            }

            else
                return HttpNotFound();
        }

        // POST: Credits/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? client, [Bind(Include = "Id, SelectedFormulaId")] CreditEditViewModel c)
        {
            if (ModelState.IsValid)
            {
                Credit credit = new Credit();
                credit.id = c.Id;
                CreditFormula CF = new CreditFormula();
                CF.id = Int32.Parse(c.SelectedFormulaId);
                credit.creditformula = CF;
                HttpClient httpClient = new HttpClient();
                httpClient.PostAsJsonAsync<CreditFormula>("http://localhost:8081/Dari/servlet/clients/1/credits/"+credit.id+"/modify", CF).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
                return RedirectToAction("Index", new { client = client });
            }
            return View(c);
        }

        // GET: Credits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/clients/1/credits/"+id+"/delete").ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult Compare(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/credits/clients/1/compare/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                Credit C = response.Content.ReadAsAsync<Credit>().Result;
                return View(C);
            }

            return RedirectToAction("Index");
        }

        // GET
        public ActionResult Email(int? id)
        {
            EmailViewModel email = new EmailViewModel();
            return View(email);
        }

        [HttpPost]
        public ActionResult Email(int? id, [Bind(Include = "Sender, Subject, Body")] EmailViewModel email)
        {
            HttpClient httpClient = new HttpClient();
            EmailViewModel test = new EmailViewModel();
            test.Body = "test";
            test.Sender="daritn4@gmail.com";
            test.Subject = "test";
            httpClient.PostAsJsonAsync<EmailViewModel>("http://localhost:8081/Dari/servlet/clients/1/credits/"+id+"/email", test).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");
        }

        public ActionResult SMS(int ?id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync("http://localhost:8081/Dari/servlet/clients/1/credits/"+id+"/sms", null).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
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
