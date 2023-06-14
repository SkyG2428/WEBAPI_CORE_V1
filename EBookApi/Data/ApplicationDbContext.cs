using EcommApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCover> BookCovers { get; set; }
        public DbSet<BookWriter> BookWriters { get; set; }

    }
}
