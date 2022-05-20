using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Controllers
{
    public class OrganizerController : Controller
    {
        HttpClient _client;
        public OrganizerController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:10433/api/organizer/");
        }

        [HttpGet]
        public async Task<IActionResult> GetMyEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("myevents");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                List<EventViewModel> result = JsonConvert.DeserializeObject<List<EventViewModel>>(content);
                return View(result);
            }
            else
            {
                string message = responseMessage.StatusCode.ToString();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> CreateEvent()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var citiesObj = await _client.GetStringAsync("cities");
            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(citiesObj);
            var selectCities = cities.Select(x => new SelectListItem { Text = x.CityName, Value = x.CityId.ToString() }).ToList();
            ViewBag.cities=selectCities;
            
            var categoriesObj = await _client.GetStringAsync("categories");
            var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoriesObj);
            var selectCategories = categories.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            ViewBag.categories=selectCategories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventModel model)
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var citiesObj = await _client.GetStringAsync("cities");
            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(citiesObj);
            var selectCities = cities.Select(x => new SelectListItem { Text = x.CityName, Value = x.CityId.ToString() }).ToList();
            ViewBag.cities = selectCities;

            var categoriesObj = await _client.GetStringAsync("categories");
            var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoriesObj);
            var selectCategories = categories.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            ViewBag.categories = selectCategories;

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await _client.PostAsync("events", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Etkinlik Oluşturuldu";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }
    }
}
