using EcommApi.Data;
using EcommApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.Controllers
{
    // Route for Query string parameter versioning
    //[Route("api/user")]
    //Route for URI versioning
    //[Route("api/v{version:apiVersion}/user/[action]")]
    //Header based Api Versioning
    [Route("api/user")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryDBController : ControllerBase
    {
        //private ApplicationDbContext _context;

        //public CategoryDBController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}





        //// GET: api/<CategoryController>
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    //return NotFount();
        //    //return BadRequest();

        //    return Ok(await _context.Categories.ToListAsync());
        //}

        //// GET api/<CategoryController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    return Ok(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
        //}

        ////POST api/<CategoryController>
        //[HttpPost]
        //public IActionResult Post([FromBody] CategoryModel category)
        //{
        //    _context.Categories.Add(category);
        //    _context.SaveChanges();
        //    return StatusCode(StatusCodes.Status201Created);
        //}

        static List<CategoryModel> categories = new List<CategoryModel>()
        {
            new CategoryModel(){Id=5, Title="Women"},
            new CategoryModel(){Id=6, Title="School"}
        };
        [HttpGet] 
        public IEnumerable<CategoryModel> Get()
        {
            return categories;
        }


    }
}
