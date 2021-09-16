using ExampleApp.Client.Data;
using ExampleApp.Context.Context;
using ExampleApp.Context.Repository;
using ExampleApp.Server.DataAccess;
using ExampleApp.Server.Filter;
using ExampleApp.Server.Interfaces;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using ExampleApp.TenantContext.Context;
using ExampleApp.TenantContext.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            SetupDefaultDatabaseContext(services);
            SetupDatabaseContext(services);
            SetupDatabase(services);

            InitialiseAuthenticationAndAuthorisation(services);

            //Does not work
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowedCorsOrigins",
                    builder =>
                    {
                        builder
                            .WithOrigins(new string[] { "localhost:5001", "pig.localhost:5001" } )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserTenantRepository, UserTenantRepository>();
            services.AddScoped(typeof(Tenant2Attribute));
            services.AddScoped<DefaultDataGenerator>();
            services.AddScoped<IWeatherForecastService, WeatherForecastDataAccessLayer>();
            services.AddTransient<IEmployee, EmployeeDataAccessLayer>();
            services.AddServerSideBlazor();
            services.AddScoped<IAlertService, AlertService>();

         

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var task = Task.Run(() => SetupDefaultData(services));
            task.Wait();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowedCorsOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static bool IsOriginAllowed(string origin)
        {
            var uri = new Uri(origin);
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "n/a";

            var isAllowed = uri.Host.Equals("localhost:5001", StringComparison.OrdinalIgnoreCase)
                            || uri.Host.Equals("pig.localhost:5001", StringComparison.OrdinalIgnoreCase)
                            || uri.Host.Equals("cat.localhost:5001", StringComparison.OrdinalIgnoreCase);
            // if (!isAllowed && env.Contains("DEV", StringComparison.OrdinalIgnoreCase))
            //     isAllowed = uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

            return false;
        }

        private static void SetupDatabase(IServiceCollection services)
        {
            using (var sp = services.BuildServiceProvider())
            {
                var context = sp.GetService<DefaultContext>();
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }
        }

        private static async Task SetupDefaultData(IServiceCollection services)
        {
            using (var sp = services.BuildServiceProvider())
            {
                var defaultDataGenerator = sp.GetService<DefaultDataGenerator>();
                await defaultDataGenerator.GenerateData();
            }
        }

        private void SetupDefaultDatabaseContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("dbConnection");
            services.AddDbContext<DefaultContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);
        }

        private void SetupDatabaseContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("dbConnection");
            services.AddDbContext<EmployeeContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);
        }

        private void InitialiseAuthenticationAndAuthorisation(IServiceCollection services)
        {
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DefaultContext>()          
            .AddDefaultTokenProviders();
         

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
        }

    }
}
