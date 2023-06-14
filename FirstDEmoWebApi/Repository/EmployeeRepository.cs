using DataAccessLayer;
using FirstDEmoWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace FirstDEmoWebApi.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDBContext _context;

		public EmployeeRepository(ApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<Employee> AddEmployees(Employee employee)
		{
			var result=await _context.Employees.AddAsync(employee);
			await _context.SaveChangesAsync();
			return result.Entity;
		}

		public async Task<Employee> Deletemployees(int Id)
		{
			var result = await _context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
			if (result != null)
			{
				_context.Employees.Remove(result);
				await _context.SaveChangesAsync();
				return result;
			}
			return null;
		}


		public async Task<Employee> GetEmployees(int Id)
		{
			return await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
		}
		public async Task<IEnumerable<Employee>> GetEmployees()
		{
			return await _context.Employees.ToListAsync();
		}

		public async Task<IEnumerable<Employee>> Search(string name)
		{
			IQueryable<Employee> query = _context.Employees;
			if(!string.IsNullOrEmpty(name))
			{
				query=query.Where(x => x.Name.Contains(name) || x.City.Contains(name));
			}
			return await query.ToListAsync();
		}

		public async Task<Employee> UpdateEmployees(Employee employee)
		{
			var result = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
			if(result!=null)
			{
				result.Name = employee.Name;
				result.City = employee.City;
				await _context.SaveChangesAsync();
				return result;
			}
			return null;
		}
	}
}
