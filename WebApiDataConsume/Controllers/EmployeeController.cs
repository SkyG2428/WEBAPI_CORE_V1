using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApiDataConsume.Models;

namespace WebApiDataConsume.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (hrm, cert, cetChain, policyErrors) => true;

            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7223/api/");            

            // var response = await client.GetAsync(client.BaseAddress + "Employee");

            string result = client.GetStringAsync(client.BaseAddress + "Employee").Result;
           // string result = await response.Content.ReadAsStringAsync();

            List <Employee> employees =
                JsonSerializer.Deserialize<List<Employee>>(result);



            return View(employees);
        }
    }
}
