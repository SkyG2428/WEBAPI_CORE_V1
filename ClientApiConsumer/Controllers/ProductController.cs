using ClientApiConsumer.SSL;
using EcommApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClientApiConsumer.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;

            HttpClient client = new HttpClient(handler);


            client .BaseAddress = new Uri("https://localhost:7218/api/");

            // var response = await client.GetAsync(client.BaseAddress + "Employee");

            string result = client.GetStringAsync(client.BaseAddress + "Products?sortPrice=asc").Result;
            // string result = await response.Content.ReadAsStringAsync();

            List<Product> products =
                JsonSerializer.Deserialize<List<Product>>(result);



            return View(products);
        }
    }
}
