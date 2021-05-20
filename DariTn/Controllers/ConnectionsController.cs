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

namespace DariTn.Controllers
{
    public class ConnectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Connections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Connections/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password")] Connection connection)
        {
            return RedirectToAction("Redirection", new { email = connection.Email, password = connection.Password });
        }


        public ActionResult Redirection(string email, string password)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/login/"+email+"/"+password).Result;
            if (response.IsSuccessStatusCode)
            {
                var u = response.Content.ReadAsAsync<User>().Result;

                if(u!=null)
                {
                    if (u.role.Equals("Administrator"))
                    {
                        return RedirectToAction("../Home/Admin");
                    }

                    else
                    {
                        if (u.role.Equals("Client"))
                        {
                            return RedirectToAction("../");
                        }
                        else
                        if (u.role.Equals("InsuranceAgent"))
                        {
                            return RedirectToAction("../InsuranceAgencies/Index");
                        }
                        else
                        if (u.role.Equals("BankAgent"))
                        {
                            return RedirectToAction("../Banks/Index");
                        }
                    }
                }

                else
                    return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
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
