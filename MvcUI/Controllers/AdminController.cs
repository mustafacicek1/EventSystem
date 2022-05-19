using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            HttpResponseMessage responseMessage = await _client.GetAsync("categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                List<CategoryViewModel> result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);
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
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CityViewModel>>(content);
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
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryModel model)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string content = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Kategori Eklendi";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("categories/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryViewModel>(content);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.DeleteAsync("categories/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Kategori Silindi";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("categories/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryViewModel>(content);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryModel model)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.PutAsync("categories/" + id, stringContent);
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
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CreateCityModel model)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string content = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("cities", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Şehir Eklendi";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("cities/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CityViewModel>(content);

            if (result==null)
            {
                return NotFound();
            }

            return View(result);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int id)
        {
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.DeleteAsync("cities/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Şehir Silindi";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.GetAsync("cities/" + id);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CityViewModel>(content);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCity(int id,UpdateCityModel model)
        {
            StringContent stringContent =new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            string accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage responseMessage = await _client.PutAsync("cities/" + id,stringContent);
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

    }
}
