using ECommApiClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommApiClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;
            _httpClient = new HttpClient(handler);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7218/api/Authenticate/Login", model);
            if(response.IsSuccessStatusCode)
            {
                var token=await response.Content.ReadAsStringAsync();
                var accessToken = JsonSerializer.Deserialize<TokenConfig>(token);
                HttpContext.Session.SetString("AccessToken", accessToken.token);
                var accessToken1 = HttpContext.Session.GetString("AccessToken");
                return RedirectToAction("Index","Home");
            }
            return View();  
            
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7218/api/Authenticate/Register", model);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}
