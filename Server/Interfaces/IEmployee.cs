using ExampleApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Server.Interfaces
{
    public interface IEmployee
    {
        public IEnumerable<Employee> GetAllEmployees();
        public void AddEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
        public Employee GetEmployeeData(int id);
        public void DeleteEmployee(int id);
    }
}
