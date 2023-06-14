﻿using Azure.Storage.Blobs;
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
    public class WritersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WritersController( ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BookWriter writer)
        {

            writer.ImageUrl =await FileHelper.UploadImage(writer.ImageFile);
            await _context.BookWriters.AddAsync(writer);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpGet]
        public async Task<IActionResult> GetWriters(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentpageSize = pageSize ?? 5;

            var writers = await (from writer in _context.BookWriters
                                 select new
                                 {
                                     Id = writer.Id,
                                     Name=writer.Name,
                                     ImageUrl = writer.ImageUrl,
                                 }).ToListAsync();
            return Ok(writers.Skip((currentPageNumber - 1) * currentpageSize).Take(currentpageSize));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> WriterDetails(int id)
        {
            var writer = await _context.BookWriters.Include(x => x.Books).
                Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(writer);
        }


    }
}
