using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Areas.Admin.ViewModels.Author;
using PB102_Consume.Areas.Admin.ViewModels.Books;
using PB102_Consume.Areas.Admin.ViewModels.Categories;
using PB102_Consume.Areas.Admin.ViewModels.Products;
using System.Text;

namespace PB102_Consume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly string BaseURl = "https://localhost:7001";
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductVM> products = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Product/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<IEnumerable<ProductVM>>(apiResponse);
                }
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{BaseURl}/api/Product/Delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ProductVM product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Product/GetById/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductVM>(apiResponse);
                }
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
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
                    multipartContent.Add(new StringContent(request.Desc), "Desc");
                    multipartContent.Add(new StringContent(request.Price.ToString()), "Price");

                    if (request.Photos != null && request.Photos.Any())
                    {
                        foreach (var photo in request.Photos)
                        {
                            var fileContent = new StreamContent(photo.OpenReadStream());
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(photo.ContentType);
                            multipartContent.Add(fileContent, "Photos", photo.FileName);
                        }
                    }

                    using (var response = await httpClient.PostAsync($"{BaseURl}/api/Product/Create", multipartContent))
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
            ProductVM product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/product/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductVM>(apiResponse);
                }
            }

            return View(new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Desc = product.Desc,
                Price = product.Price
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVM request)
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
                    multipartContent.Add(new StringContent(request.Desc), "Desc");
                    multipartContent.Add(new StringContent(request.Price.ToString()), "Price");

                    if (request.Photos != null && request.Photos.Any())
                    {
                        foreach (var photo in request.Photos)
                        {
                            var fileContent = new StreamContent(photo.OpenReadStream());
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(photo.ContentType);
                            multipartContent.Add(fileContent, "Photos", photo.FileName);
                        }
                    }

                    using (var response = await httpClient.PutAsync($"{BaseURl}/api/product/edit/{id}", multipartContent))
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
        public async Task<IActionResult> AddCategory()
        {
            AddCategoryVM model = new AddCategoryVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/Product/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Products = JsonConvert.DeserializeObject<List<ProductVM>>(apiResponse);
                }

                using (var categoryResponse = await httpClient.GetAsync($"{BaseURl}/api/category/getall"))
                {
                    string categoryApiResponse = await categoryResponse.Content.ReadAsStringAsync();
                    model.Categories = JsonConvert.DeserializeObject<List<CategoryVM>>(categoryApiResponse);
                }
            }

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddCategoryVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{BaseURl}/api/product/addcategory/{request.ProductId}/{request.CategoryId}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "API-də xəta baş verdi.");
                        return View(request);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
