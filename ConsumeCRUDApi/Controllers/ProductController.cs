using ConsumeCRUDApi.SSL;
using ConsumeCRUDApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace ConsumeCRUDApi.Controllers
{
	public class ProductController : Controller
	{
		Uri baseAddress = new Uri("http://localhost:8088/api/"); //44355

		private SSLCertificate ssl;
        public ProductController()
		{
			ssl = new SSLCertificate();

            ssl.client.BaseAddress = baseAddress;
		}

		[HttpGet]
		public IActionResult Index()
		{

				string result = ssl.client.GetStringAsync(ssl.client.BaseAddress + "Product/Get").Result;
			List<Product> products =
				JsonSerializer.Deserialize<List<Product>>(result);

			return View(products);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Create")]
		public IActionResult CreateData(Product model)
		{
			try
			{
				string data = JsonSerializer.Serialize(model);
				StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
				HttpResponseMessage responce = ssl.client.PostAsync(ssl.client.BaseAddress + "Product/Post", Content).Result;

				if (responce.IsSuccessStatusCode)
				{
					TempData["successMessage"] = "Product Created";
					return RedirectToAction("Index");
				}

				
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return View();
			}
			return View();
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			try
			{
				Product product = new Product();
				HttpResponseMessage response = ssl.client.GetAsync(ssl.client.BaseAddress + "Product/Get/" + id).Result;

				if (response.IsSuccessStatusCode)
				{
					string data = response.Content.ReadAsStringAsync().Result;
					product = JsonSerializer.Deserialize<Product>(data);
				}
                return View(product);
            }
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return View();
			}

			
		}

		[HttpPost]
		public IActionResult Edit(Product model)
		{
			try
			{
				string data = JsonSerializer.Serialize(model);
				StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
				HttpResponseMessage responce = ssl.client.PutAsync(ssl.client.BaseAddress + "Product/Put", Content).Result;

				if (responce.IsSuccessStatusCode)
				{
					TempData["success"] = "Product Details Update Successfully..";
					return RedirectToAction("Index");
				}
			}

            catch(Exception ex)

            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

		[HttpGet]
		public IActionResult Delete(int id)
		{
			try
			{
				Product product = new Product();
                HttpResponseMessage response = ssl.client.GetAsync(ssl.client.BaseAddress + "Product/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
				{
					string data = response.Content.ReadAsStringAsync().Result; 
					product = JsonSerializer.Deserialize<Product>(data);
				}

				return View(product);
			}
            catch (Exception ex)

            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }


		[HttpPost , ActionName("Delete")]
		public IActionResult DeleteConfirm(int id)
		{
			try
			{
				HttpResponseMessage response = ssl.client.DeleteAsync(ssl.client.BaseAddress + "Product/Delete?" +$"id={id}").Result;
				if (response.IsSuccessStatusCode)
				{
                    TempData["success"] = "Product Delete Successfully..";
                    return RedirectToAction("Index");
				}
			}
            catch (Exception ex)

            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }


	}
}
