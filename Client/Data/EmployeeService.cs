using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;

namespace ExampleApp.Client.Data
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private const string EMPLOYEE_ROUTE = "/api/Employee/";
        private const string Tenant_ROUTE = "/api/Tenant/";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _httpClient.PostAsJsonAsync($"{EMPLOYEE_ROUTE}Create", employee);
        }    

        public async Task DeleteEmployee(int id)
        {
            await _httpClient.DeleteAsync($"{EMPLOYEE_ROUTE}Delete/{id}");
        }

        public async Task<Employee[]> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<Employee[]>($"{EMPLOYEE_ROUTE}Index");
        }

        public async Task<Employee> GetEmployeeData(int id)
        {
            var employeeResponse = await _httpClient.GetAsync($"{EMPLOYEE_ROUTE}Details/{id}");
            var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
            return employee;
        }
        public async Task<string> GetAllTenants()
        {
            return await _httpClient.GetFromJsonAsync<string>($"{Tenant_ROUTE}");
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _httpClient.PutAsJsonAsync($"{EMPLOYEE_ROUTE}Edit", employee);
        }
    }
}