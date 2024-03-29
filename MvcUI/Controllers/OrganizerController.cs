﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int? id)
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
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventModel model)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.PatchAsync("events/" + id, stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Güncelleme başarılı";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CancelEvent(int? id)
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
        public async Task<IActionResult> CancelEvent(int id)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var eventObj = await _client.GetStringAsync("events/" + id);
            var evnt = JsonConvert.DeserializeObject<EventViewModel>(eventObj);


            HttpResponseMessage responseMessage = await _client.PostAsync("cancelevent/" + id,null);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Event iptal edildi";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View(evnt);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("UserLogin", "Login");
        }
    }
}
