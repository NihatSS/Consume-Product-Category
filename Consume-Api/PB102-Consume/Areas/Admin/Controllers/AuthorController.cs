using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Areas.Admin.ViewModels.Author;
using PB102_Consume.Areas.Admin.ViewModels.Books;
using System.Text;

namespace PB102_Consume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly string BaseURl = "https://localhost:7001";
        public async Task<IActionResult> Index()
        {
            IEnumerable<AuthorVM> author = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Author/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<IEnumerable<AuthorVM>>(apiResponse);
                }
            }
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{BaseURl}/api/author/delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            AuthorVM author = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/author/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<AuthorVM>(apiResponse);
                }
            }

            return View(author);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            using (var httpClient = new HttpClient())
            {
                using (var multipartContent = new MultipartFormDataContent())
                {
                    multipartContent.Add(new StringContent(request.Name), "Name");
                    multipartContent.Add(new StringContent(request.Surname), "Surname");
                    multipartContent.Add(new StringContent(request.Age.ToString()), "Age");

                    if (request.Image != null)
                    {
                        var fileContent = new StreamContent(request.Image.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.Image.ContentType);
                        multipartContent.Add(fileContent, "Image", request.Image.FileName);
                    }

                    using (var response = await httpClient.PostAsync($"{BaseURl}/api/author/create", multipartContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            ModelState.AddModelError(string.Empty, "API-də xəta baş verdi.");
                            return View(request);
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookVM book = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/book/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<BookVM>(apiResponse);
                }
            }

            return View(new BookEditVM { Id = book.Id, Name = book.Name, PageCount = book.PageCount });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{BaseURl}/api/book/edit/{id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
