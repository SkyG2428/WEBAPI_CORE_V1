using FirstDEmoWebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DataAccessLayer;

namespace FirstDEmoWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeController( IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetEmployees()
		{
			try
			{
				return Ok(await _employeeRepository.GetEmployees());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");
			}


		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Employee>> GetEmployees(int id)
		{
			try
			{
				var result= await _employeeRepository.GetEmployees(id);
				if(result==null)
				{
					return NotFound();
				}
				return result;
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");
			}


		}

		[HttpPost]
		public async Task<ActionResult<Employee>> CreateEmployees(Employee employee)
		{
			try
			{
				if(employee==null)
				{
					return BadRequest();
				}
				var createEmployee=await _employeeRepository.AddEmployees(employee);
				return CreatedAtAction(nameof(GetEmployees), new { id = createEmployee.Id }, createEmployee);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");
			}
		}


		[HttpPut("{id:int}")]
		public async Task<ActionResult<Employee>> UpdateEmployees(int id,Employee employee)
		{
			try
			{
				if(id != employee.Id)
				{
					return BadRequest("Id Mismatch"); 
				}
				var updateEmployee = await _employeeRepository.GetEmployees(id);
				if(updateEmployee==null)
				{
					return NotFound($"Employee Id={id} Not Found");

				}


				
				return await _employeeRepository.UpdateEmployees(employee);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");
			}
		}


		[HttpDelete("{id:int}")]
		public async Task<ActionResult<Employee>> DeleteEmployees(int id)
		{
			try
			{
				
				var DeleteEmployee = await _employeeRepository.GetEmployees(id);
				if (DeleteEmployee == null)
				{
					return NotFound($"Employee Id={id} Not Found");

				}



				return await _employeeRepository.Deletemployees(id);

			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");
			}
		}

		[HttpGet("{Search}")]
		public async Task<ActionResult<IEnumerable<Employee>>> Search(string name)
		{
			try
			{
				var result=await _employeeRepository.Search(name);
				if(result.Any())
				{
					return Ok(result);
				}
				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error in Retrieving Data from Database");

			}
		}

	}
}
