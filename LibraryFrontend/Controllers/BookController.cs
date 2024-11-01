using LibraryFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace LibraryFrontend.Controllers
{
    public class BookController : Controller
    {

        string Baseurl = "http://ec2-3-80-110-216.compute-1.amazonaws.com/";


        // GET: Book
        public async Task<ActionResult> Index()
        {
            

            List<Book> BookInfo = new List<Book>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Book");

                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    
                    BookInfo = JsonConvert.DeserializeObject<List<Book>>(preResponse);
                }
                return View(BookInfo);
            }
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Book book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Book/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(preResponse);
                }
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var json = JsonConvert.SerializeObject(book);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Book", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to create the book. Please try again.");
            }
            return View(book);
        }



        // GET: Book/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Book book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Book/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(preResponse);
                }
            }
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var json = JsonConvert.SerializeObject(book);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PutAsync($"api/Book/{id}", content);
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to edit the book. Please try again.");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync($"api/Book/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    var preResponse = Res.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(preResponse);
                }
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.DeleteAsync($"api/Book/{id}");
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete the book. Please try again.");
            }
            return View();
        }
    }
}
