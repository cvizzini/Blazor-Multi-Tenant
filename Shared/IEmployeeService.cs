using ExampleApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.Shared
{
    public interface IEmployeeService
    {
        Task<Employee[]> GetAllEmployees();
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeData(int id);
        Task DeleteEmployee(int id);
        Task<string> GetAllTenants();
    }
}