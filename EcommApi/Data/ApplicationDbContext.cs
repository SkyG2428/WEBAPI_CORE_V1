using EcommApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //public DbSet<Book> Books { get; set; }
        //public DbSet<BookCover> BookCovers { get; set; }
        //public DbSet<BookWriter> BookWriters { get; set; }

    }
}
