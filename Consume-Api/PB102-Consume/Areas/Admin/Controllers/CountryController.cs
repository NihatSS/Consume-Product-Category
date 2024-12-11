using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Areas.Admin.ViewModels.Countries;
using PB102_Consume.Models;
using System.Diagnostics.Metrics;
using System.Text;

namespace PB102_Consume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly string BaseURl = "https://localhost:7001";
        public async Task<IActionResult> Index()
        {
            IEnumerable<CountryVM> countries = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/country/getall"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<IEnumerable<CountryVM>>(apiResponse);
                }
            }
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{BaseURl}/api/country/create", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{BaseURl}/api/country/delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CountryVM country = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/country/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    country = JsonConvert.DeserializeObject<CountryVM>(apiResponse);
                }
            }

            return View(new CountryEditVM { Id = country.Id, Name = country.Name, Population = country.Population });
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{BaseURl}/api/country/edit/{id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            CountryVM country = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/country/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    country = JsonConvert.DeserializeObject<CountryVM>(apiResponse);
                }
            }

            return View(country);
        }
        [HttpGet]
        public async Task<IActionResult> Search(CountryVM request)
        {
            IEnumerable<CountryVM> countries = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/country/search/" + request.Name))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<IEnumerable<CountryVM>>(apiResponse);
                }
            }
            return View(countries);
            
        }
    }
}
