using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace FirstDEmoWebApi.DataContext
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
