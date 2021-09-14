using ExampleApp.Context.Context;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Server.DataAccess
{
    public class DefaultDataGenerator
    {
        private readonly EmployeeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DefaultDataGenerator(EmployeeContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task GenerateData()
        {            
            await GenerateDefaultUser();
            await GenerateTenants();
        }

        private async Task GenerateDefaultUser()
        {
            var user = new ApplicationUser()
            {
                Email = "admin@email.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Admin"
            };

            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser == null)
                await _userManager.CreateAsync(user, "P@ssword123");
        }

        private async Task GenerateTenants()
        {
            var tenants = new List<Tenant>()
            {
                new Tenant() { Host = "Pig.localhost", Name = "Pig", TenantId = 1 , DatabaseConnection = "Server=localhost;Database=evet.Pig;UID=TestUser;Password=HRG_wzm!twt3gqv2jtq"},
                new Tenant() { Host = "Dog.localhost", Name = "Dog", TenantId = 2 , DatabaseConnection = "Server=localhost;Database=evet.Dog;UID=TestUser;Password=HRG_wzm!twt3gqv2jtq" },
            };
            using var dbContext = _context.Create();
            foreach (var tenant in tenants)
            {
                var existing = await dbContext.Tenants.FirstOrDefaultAsync(x => x.TenantId == tenant.TenantId);
                if (existing == null)
                {
                    dbContext.Tenants.Add(tenant);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
