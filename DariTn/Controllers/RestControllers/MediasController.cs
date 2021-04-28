using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DariTn.Models.Entities;
using DariTN.Models.Entities;

namespace DariTn.Controllers.RestControllers
{
    public class MediasController : Controller
    {
        // GET: Medias
        public ActionResult Index()
        {
            return View();
        }

        // GET: Medias
        public ActionResult Show(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:8081/Dari/servlet/Media/" + id + "/image").Result;
            return Redirect("http://localhost:8081/Dari/servlet/Media/" + id + "/image");


        }

        // GET: Medias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Medias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medias/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Medias/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage tokenResponse = httpClient.GetAsync("http://localhost:8081/Dari/servlet/getMedia/" + id).Result;
            var med = tokenResponse.Content.ReadAsAsync<Media>().Result;
            if (med == null)
            {
                return HttpNotFound();
            }
            return View(med);
        }

        // POST: Medias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Media med)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            HttpResponseMessage tokenResponse = httpClient.PutAsJsonAsync<Media>("http://localhost:8081/Dari/servlet/Media/" + id + "/modify" , med).Result;
            return RedirectToAction("MyAdvs","AssetAdvs");
        }

        // GET: Medias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DeleteAsync("http://localhost:8081/Dari/servlet/Media/delete/" + id).ContinueWith(postTask => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("MyAdvs","AssetAdvs");
        }


    }
}
