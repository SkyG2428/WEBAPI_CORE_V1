using CreudOperationWebApi.DataAccess;
using CreudOperationWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreudOperationWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				var product = _context.Products.ToList();
				if (product.Count == 0)
				{
					return NotFound("Product Not Available");
				}
				return Ok(product);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			try
			{
				var products = _context.Products.Find(id);
				if (products == null)
				{
					return NotFound($"Product Details Not Found with id {id}");
				}

				return Ok(products);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult Post(ProductModel model)
		{
			try
			{
				_context.Add(model);
				_context.SaveChanges();
				return Ok("Product Created Successfully..");
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public IActionResult Put(ProductModel model)
		{
			try
			{
				if (model == null || model.Id == 0)
				{
					if (model == null)
					{
						return BadRequest("Model data is invalid.");
					}
					else if (model.Id == 0)
					{
						return BadRequest($" Product Id {model.Id} is invalid");
					}
				}
				var product = _context.Products.Find(model.Id);
				if (product == null)
				{
					return NotFound($" Product not found with Id {model.Id}");
				}
				product.Name = model.Name;
				product.Price = model.Price;
				product.Quantity = model.Quantity;
				_context.SaveChanges();

				return Ok("Product Details Updated successfully..");
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		public IActionResult Delete(int id)
		{
			try
			{
				var model = _context.Products.Find(id);
				if (model == null)
				{
					return NotFound($"Product not found with id {id}");

				}
				_context.Products.Remove(model);
				_context.SaveChanges();
				return Ok("Delete Successfully..");
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

	}
}
