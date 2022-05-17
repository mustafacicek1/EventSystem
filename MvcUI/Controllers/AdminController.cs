using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcUI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcUI.Controllers
{
    public class AdminController : Controller
    {
        HttpClient _client;
        public AdminController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:10433/api/admin/");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage =await  _client.GetAsync("categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content= await responseMessage.Content.ReadAsStringAsync();
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                serializerOptions.PropertyNameCaseInsensitive = true;
                List<CategoryViewModel> result = JsonSerializer.Deserialize<List<CategoryViewModel>>(content, serializerOptions);
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
        public async Task<ActionResult> GetCities()
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("cities");
            if(responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                serializerOptions.PropertyNameCaseInsensitive=true;
                var result = JsonSerializer.Deserialize<List<CityViewModel>>(content, serializerOptions);
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
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CreateCityModel model)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string content = JsonSerializer.Serialize(model);
            StringContent stringContent = new StringContent(content,Encoding.UTF8,"application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("cities",stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Şehir Eklendi";
            }
            else if(responseMessage.StatusCode==HttpStatusCode.Unauthorized || responseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                string message = responseMessage.StatusCode.ToString();
                ViewBag.message = message;
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
