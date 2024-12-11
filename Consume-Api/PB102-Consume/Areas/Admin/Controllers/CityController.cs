using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Areas.Admin.ViewModels.Cities;
using System.Text;

namespace PB102_Consume.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly string BaseURl = "https://localhost:7001";
        public async Task<IActionResult> Index()
        {
            IEnumerable<CityVM> cities = null;
            using (var httpClient = new HttpClient())
            { 
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/City/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<IEnumerable<CityVM>>(apiResponse);
                }
            }
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{BaseURl}/api/city/create", content))
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
                using (var response = await httpClient.DeleteAsync($"{BaseURl}/api/city/delete/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            CityVM city = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BaseURl}/api/city/getbyid/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    city = JsonConvert.DeserializeObject<CityVM>(apiResponse);
                }
            }

            return View(city);
        }
    }
}
