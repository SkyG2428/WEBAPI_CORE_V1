using EmailConsumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmailConsumer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult SendEmailAction()
		{
			return View();
		}



		[HttpPost]
		[ActionName("SendEmailAction")]
		public async Task<IActionResult> SendEmail(Email email)
		{
			string apiUrl = "http://113.193.63.106:4050/api/Email"; 
			//string apiUrl = "http://113.193.63.106:4050/swagger/ui/index#!/Email/Email_SendEmail";


			//using (HttpClient client = new HttpClient())
			//{

			//	var emailModel = new Email
			//	{
			//		ToEmail = email.ToEmail,
			//		EmailSubject = email.EmailSubject,
			//		EmailBody = email.EmailBody,
			//		IsBodyHtml= true,
			//	};


			//	HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, emailModel);


			//	if (response.IsSuccessStatusCode)
			//	{

			//		return View();
			//	}
			//	else
			//	{

			//		string errorMessage = await response.Content.ReadAsStringAsync();
			//		return View("Error", errorMessage);
			//	}
			//}
			HttpClient client = new HttpClient();
			
			var emailModel = new Email
			{
				ToEmail = email.ToEmail,
				EmailSubject = email.EmailSubject,
				EmailBody = email.EmailBody,
				IsBodyHtml = true,
			};


			HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, emailModel);


			if (response.IsSuccessStatusCode)
			{

				return View();
			}
			else
			{

				string errorMessage = await response.Content.ReadAsStringAsync();
				return View("Error", errorMessage);
			}
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