using LibraryFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace LibraryFrontend.Controllers
{
    public class AuthorController : Controller
    {

        private const string Baseurl = "http://localhost:5206/";

        // GET: Author
        public async Task<ActionResult> Index()
        {
            List<Author> AuthorInfo = new List<Author>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Author");

                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    AuthorInfo = JsonConvert.DeserializeObject<List<Author>>(preResponse);
                }
            }
            return View(AuthorInfo);
        }

        // GET: Author/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Author/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(preResponse);
                }
            }
            return View(author);
        }


        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var json = JsonConvert.SerializeObject(author);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Author", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to create the author. Please try again.");
            }
            return View(author);
        }

       

        // POST: Author/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Author author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Author/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(preResponse);
                }
            }
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Author author)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var json = JsonConvert.SerializeObject(author);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PutAsync($"api/Author/{id}", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to edit the author. Please try again.");
            }
            return View(author);
        }

        // GET: Author/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Author author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Author/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(preResponse);
                }
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.DeleteAsync($"api/Author/{id}");
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete the author. Please try again.");
            }
            return View();
        }
    }
}
