using ExampleApp.Context.Context;
using ExampleApp.Server.Interfaces;
using ExampleApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ExampleApp.Server.DataAccess
{
    public class EmployeeDataAccessLayer : IEmployee
    {
        public EmployeeDataAccessLayer(EmployeeContext employeeContext)
        {
            db = employeeContext;
        }
        private readonly EmployeeContext db;

        //To Get all employees details   
        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return db.Employees.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new employee record     
        public void AddEmployee(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar employee    
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular employee    
        public Employee GetEmployeeData(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                return employee;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular employee    
        public void DeleteEmployee(int id)
        {
            try
            {
                Employee emp = db.Employees.Find(id);
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
