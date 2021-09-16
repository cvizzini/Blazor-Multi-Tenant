using ExampleApp.Shared.Models;
using ExampleApp.TenantContext.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ExampleApp.Context.Context
{
    public class EmployeeContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<EmployeeContext> _options;
        private readonly IHttpContextAccessor _accessor;
        private readonly DefaultContext _defaultContext;
        private Tenant _tenant;

        public EmployeeContext(IConfiguration configuration, DbContextOptions<EmployeeContext> options, IHttpContextAccessor accessor, DefaultContext defaultContext, Tenant tenant = null) : base(options)
        {
            this._configuration = configuration;
            _options = options;
            _accessor = accessor;
            _defaultContext = defaultContext;
            _tenant = tenant;
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<BankDetail> BankDetails { get; set; }
        public virtual DbSet<JobDetail> JobDetails { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _tenant?.DatabaseConnection ?? _configuration.GetConnectionString("dbConnection");
            if (_tenant != null)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
            else
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }      

        public EmployeeContext Create()
        {
            var hostValue = _accessor.HttpContext?.Request?.Host.Value.ToLower() ?? "";
            var defaultContext = _defaultContext.Create();
            var tenant = defaultContext.Tenants?.FirstOrDefault(t => t.Host.ToLower() == hostValue);
            var dbContext = new EmployeeContext(_configuration, _options, _accessor, _defaultContext, tenant);
            if (tenant != null)
            {
                dbContext.Database.Migrate();
            }

            return dbContext;
        }



    }
}
