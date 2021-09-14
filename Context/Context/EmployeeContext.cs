using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ExampleApp.Context.Context
{
    public class EmployeeContext :  IdentityDbContext<ApplicationUser> 
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<EmployeeContext> _options;

        public EmployeeContext(IConfiguration configuration, DbContextOptions<EmployeeContext> options) : base(options)
        {
            this._configuration = configuration;
            _options = options;
        }

      
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            var server = _configuration.GetConnectionString("Server");
            var database = _configuration.GetConnectionString("Database");
            var user = _configuration.GetConnectionString("User");
            var password = _configuration.GetConnectionString("Password");
            //var connectionString = $"server={server};database={database};user={user};password={password}";
            var connectionString = _configuration.GetConnectionString("dbConnection");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        public EmployeeContext Create()
        {
            return new EmployeeContext(_configuration, _options);
        }

    }
}
