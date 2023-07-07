using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XUnit_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        //public string Index()
        //{
        //    return "X Unit Test Demo Tryel";
        //}

        public string Index(int gussedNumber)
        {
           if(gussedNumber < 100)
           {
                return "Wrong! Try a bigger Number.";
           }
           else if(gussedNumber > 100)
           {
                return "Wrong! Try a smaller Number.";
           }
           else
           {
                return "You guessed correct number..";
           }
        }
    }
}
