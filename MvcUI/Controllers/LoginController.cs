using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcUI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Controllers
{
    public class LoginController : Controller
    {
        HttpClient _client;
        public LoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:10433/api/auth/");
        }

        [HttpGet]
        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterModel userRegisterModel)
        {
            string content = JsonConvert.SerializeObject(userRegisterModel);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("user/register", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.message = "Kayıt başarılı";
            }
            else
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.message = message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginModel userLoginModel)
        {
            string content = JsonConvert.SerializeObject(userLoginModel);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync("user/login", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var tokenModel = await responseMessage.Content.ReadAsStringAsync();
                TokenViewModel tokenViewModel = JsonConvert.DeserializeObject<TokenViewModel>(tokenModel);
                ViewBag.message = "Giriş başarılı";
                HttpContext.Session.SetString("token", tokenViewModel.Token);
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
