using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PB102_Consume.Models;

namespace PB102_Consume.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        { 
            IEnumerable<Country> countries = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7001/api/country/getall"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(apiResponse);
                }
            }

            return View(countries);
        }


    }
}
