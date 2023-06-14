using EcommApi.Data;
using EcommApi.Helper;
using EcommApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Book book)
        {

            book.ImageUrl = await FileHelper.UploadImage(book.ImageFile);
            book.BookUrl = await FileHelper.UploadUrl(book.BookFile);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }



        [HttpGet]
        public async Task<IActionResult> GetBooks(int? pageNumber, int? pageSize )
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentpageSize=pageSize ?? 5;

            var books = await (from book in _context.Books
                                select new
                                {
                                    Id = book.Id,
                                    Name = book.Title,
                                    ImageUrl = book.ImageUrl,
                                    BookUrl = book.BookUrl,
                                    Description= book.Description,

                                }).ToListAsync();
            return Ok(books.Skip((currentPageNumber-1)*currentpageSize).Take(currentpageSize));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> BookDetails(int id)
        {
            var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(book);
        }


        [HttpGet ("[action]")]
        public async Task<IActionResult> TrendingBooks()
        {
            var books = await (from book in _context.Books
                               where book.Trending == true
                               select new
                               {
                                   Id = book.Id,
                                   Name = book.Title,
                                   ImageUrl = book.ImageUrl,
                                   BookUrl = book.BookUrl,
                                   Description = book.Description,

                               }).ToListAsync();
            return Ok(books);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewBooks()
        {
            var books = await (from book in _context.Books
                               orderby book.CreatedDate descending
                               select new
                               {
                                   Id = book.Id,
                                   Name = book.Title,
                                   ImageUrl = book.ImageUrl,
                                   BookUrl = book.BookUrl,
                                   Description = book.Description,

                               }).Take(5).ToListAsync();
            return Ok(books);
        }


        //api/books/searchbook?query=""
        [HttpGet("[action]")]
        public async Task<IActionResult> SearchBooks(string q)
        {
            var books = await (from book in _context.Books
                               where book.Title.StartsWith(q)
                               select new
                               {
                                   Id = book.Id,
                                   Name = book.Title,
                                   ImageUrl = book.ImageUrl,
                                   BookUrl = book.BookUrl,
                                   Description = book.Description,

                               }).Take(5).ToListAsync();
            return Ok(books);
        }

    }
}
