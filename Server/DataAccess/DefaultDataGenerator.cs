using ExampleApp.Context.Context;
using ExampleApp.Shared.Models;
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

        public DefaultDataGenerator(EmployeeContext context)
        {
            _context = context;
        }

        public async void GenerateData()
        {
            var tenants = new List<Tenant>()
            {
                new Tenant() { Host = "Pig", Name = "Pig", TenantId = 1 },
                new Tenant() { Host = "Dog", Name = "Dog", TenantId = 2 },
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
