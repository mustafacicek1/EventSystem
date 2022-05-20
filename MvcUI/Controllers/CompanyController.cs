using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcUI.Controllers
{
    public class CompanyController : Controller
    {
        HttpClient _client;
        public CompanyController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:10433/api/company/");
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var accessToken = HttpContext.Session.GetString("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("CompanyLogin", "Login");
        }
    }
}
