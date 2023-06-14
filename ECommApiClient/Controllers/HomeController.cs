using ECommApiClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace ECommApiClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;

            //HttpClient client = new HttpClient(handler);
            _logger = logger;
            _httpClient = new HttpClient(handler);


          
            //client.BaseAddress = new Uri("https://localhost:7223/api/");
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Getproduct()
        {



            var accessToken = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = _httpClient.GetAsync("https://localhost:7218/api/Products?sortPrice=desc").Result;

            var booksString= response.Content.ReadAsStringAsync().Result;

            var books = JsonSerializer.Deserialize<List<Product>>(booksString);

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}