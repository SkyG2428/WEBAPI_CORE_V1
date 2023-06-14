using DataAccessLayer;

namespace FirstDEmoWebApi.Repository
{
	public interface IEmployeeRepository
	{

		//Task = Entity 
		Task<IEnumerable<Employee>> Search(string name);//multiple employee
		Task<IEnumerable<Employee>> GetEmployees();//multiple employee
		Task<Employee> GetEmployees(int Id);//perticular employee using id
		Task<Employee> AddEmployees(Employee employee);//add one employee record
		Task<Employee> UpdateEmployees(Employee employee);//Update perticular data 
		Task<Employee> Deletemployees(int Id);//Delete employee using id


	}
}
