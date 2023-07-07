using CreudOperationWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CreudOperationWebApi.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<ProductModel> Products { get; set; }
	}
}
