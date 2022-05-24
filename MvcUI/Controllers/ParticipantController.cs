using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcUI.Controllers
{
    public class ParticipantController : Controller
    {
        HttpClient _client;
        public ParticipantController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:10433/api/participant/");
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            ViewBag.token=JsonConvert.SerializeObject(accessToken);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var citiesObj = await _client.GetStringAsync("cities");
            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(citiesObj);
            var selectCities = cities.Select(x => new SelectListItem { Text = x.CityName, Value = x.CityId.ToString() }).ToList();
            ViewBag.cities = selectCities;

            var categoriesObj = await _client.GetStringAsync("categories");
            var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoriesObj);
            var selectCategories = categories.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId.ToString() }).ToList();
            ViewBag.categories = selectCategories;

            HttpResponseMessage responseMessage = await _client.GetAsync("events");
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
        public async Task<IActionResult> GetJoinedEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("events/joined");
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
        public async Task<IActionResult> GetCouldntJoinedEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("events/couldntjoined");
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
        public async Task<IActionResult> GetCanceledEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("events/canceled");
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
        public async Task<IActionResult> JoinEvent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("events/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EventViewModel>(content);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> JoinEvent(int id)
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var eventObj = await _client.GetStringAsync("events/" + id);
            var evnt = JsonConvert.DeserializeObject<EventViewModel>(eventObj);

            HttpResponseMessage responseMessage = await _client.PostAsync("events/join/"+id,null);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Etkinliğe başarıyla katıldınız";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View(evnt);
        }

        [HttpGet]
        public async Task<IActionResult> CancelJoinEvent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("events/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EventViewModel>(content);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CancelJoinEvent(int id)
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var eventObj = await _client.GetStringAsync("events/" + id);
            var evnt = JsonConvert.DeserializeObject<EventViewModel>(eventObj);

            HttpResponseMessage responseMessage = await _client.PostAsync("events/canceljoin/" + id, null);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Etkinliğe katılımınızı iptal ettiniz";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View(evnt);
        }
    }
}
