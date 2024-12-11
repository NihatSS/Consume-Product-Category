using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Areas.Admin.ViewModels.Author;
using PB102_Consume.Areas.Admin.ViewModels.Books;
using PB102_Consume.Areas.Admin.ViewModels.Cities;
using PB102_Consume.Areas.Admin.ViewModels.Countries;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace PB102_Consume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly string BaseURl = "https://localhost:7001";
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookVM> books = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Book/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<IEnumerable<BookVM>>(apiResponse);
                }
            }
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{BaseURl}/api/book/delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            BookVM books = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/book/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<BookVM>(apiResponse);
                }
            }

            return View(books);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{BaseURl}/api/book/create", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
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

        [HttpGet]
        public async Task<IActionResult> AddAuthor()
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

            IEnumerable<BookVM> books = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Book/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<IEnumerable<BookVM>>(apiResponse);
                }
            }

            return View(new AddAuthorVM { Books = books, Authors = author });
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(int bookId, int authorId)
        {
            var payload = new { BookId = bookId, AuthorId = authorId };

            using (var httpClient = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{BaseURl}/api/Book/AddAuthorToBook?bookId={bookId}&authorId={authorId}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var books = JsonConvert.DeserializeObject<IEnumerable<BookVM>>(apiResponse);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add author to book.");
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
