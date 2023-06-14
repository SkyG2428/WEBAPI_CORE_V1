using Azure.Storage.Blobs;
using EcommApi.Data;
using EcommApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommApi.Controllers
{
    [Authorize(Roles = "Admin")]
    //Route for Query string parameter versioning
    [Route("api/[controller]")]


    //Route for URI versioning
    //[Route("api/v{version:apiVersion}/user/[action]")]

    //Header based Api Versioning
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiVersion("2.0")]
    public class CategoriesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }





        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return NotFount();
            //return BadRequest();

            return Ok(await _context.Categories.ToListAsync());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }

        // POST api/<CategoryController>
        //[HttpPost]
        //public IActionResult Post([FromBody] CategoryModel category)
        //{
        //    _context.Categories.Add(category);
        //    _context.SaveChanges();
        //    return StatusCode(StatusCodes.Status201Created);
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CategoryModel category)
        {
            string filename = Guid.NewGuid().ToString();

            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=skyindia;AccountKey=BNTVkxbaRasLc6fk3BUi4LqOI4INGOCWPZUoUMDoyq3TaOi99bLVpqV6uYfPSb/jbtof20GWmTGU+ASt3e+IfQ==;EndpointSuffix=core.windows.net";
            string containerName = "skyindiaimage";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename + category.CategoryImage.FileName);

            MemoryStream ms = new MemoryStream();
            await category.CategoryImage.CopyToAsync(ms);
            ms.Position = 0;
            await blobClient.UploadAsync(ms);
            category.CategoryImagePath = blobClient.Uri.AbsoluteUri;

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryModel category)
        {
            var model = _context.Categories.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                model.Title = category.Title;
                model.DisplayOrder = category.DisplayOrder;
                model.CreatedDate = DateTime.Now;
                _context.Categories.Update(model);
                _context.SaveChanges();
                return Ok("Category Updated");
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.Categories.Find(id);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                _context.Categories.Remove(model);
                _context.SaveChanges();
                return Ok("Category Deleted");
            }
        }
    }
}
