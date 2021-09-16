using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ExampleApp.TenantContext.Context
{
    public class DefaultContext : IdentityDbContext<ApplicationUser> //: IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<DefaultContext> _options;

        public DefaultContext(IConfiguration configuration, DbContextOptions<DefaultContext> options) : base(options)
        {
            _configuration = configuration;
            _options = options;            
        }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<UserTenantAccess> UserTenantAccess { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("dbConnection");
         
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        public DefaultContext Create()
        {          
            var dbContext = new DefaultContext(_configuration, _options);        

            return dbContext;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         

        }

    }
}
