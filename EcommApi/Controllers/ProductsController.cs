using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommApi.Data;
using EcommApi.Models;
using EcommApi.Repository;
using EcommApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace EcommApi.Controllers
{

    //[ApiKeyAuthentication]
    [Authorize(Roles ="Admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _product;

        public ProductsController(IProductRepository product)
        {
            _product = product;
        }

        //[HttpGet]
        //public  IActionResult Get()
        //{
        //    //return NotFount();
        //    //return BadRequest();
        //    var products =(IQueryable<Product>)_product.GetAll();

        //    return Ok(products);
        //}
        // GET: api/Products
        [HttpGet]
        public IQueryable<Product> GetProducts(string sortPrice)
        {
            IQueryable<Product> products;
            switch(sortPrice)
            {
                case "desc":
                    products = (IQueryable<Product>)_product.GetAll().
                        OrderByDescending(x => x.Price);
                    break;

                case "asc":
                    products = (IQueryable<Product>)_product.GetAll().
                        OrderBy(x => x.Price);
                    break;


                default:
                    products = (IQueryable<Product>)_product.GetAll();
                    break;
            }


            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
          
            var product = _product.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }


            var productbyId = _product.GetProductById(id);
            _product.Update(productbyId);

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {

            _product.Insert(product);
            

            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product=_product.GetProductById(id);
            _product.Delete(product);
            return Ok();
        }

        //private bool ProductExists(int id)
        //{
        //    return (_product.GetProductById?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
