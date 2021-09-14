using ExampleApp.Client.Data;
using ExampleApp.Context.Context;
using ExampleApp.Server.DataAccess;
using ExampleApp.Server.Filter;
using ExampleApp.Server.Interfaces;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;
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
           

            SetupDatabaseContext(services);
            SetupDatabase(services);

            InitialiseAuthenticationAndAuthorisation(services);

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

        private static void SetupDatabase(IServiceCollection services)
        {
            using (var sp = services.BuildServiceProvider())
            {
                var context = sp.GetService<EmployeeContext>();
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

        private void SetupDatabaseContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("dbConnection");
            services.AddDbContext<EmployeeContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);
        }

        private void InitialiseAuthenticationAndAuthorisation(IServiceCollection services)
        {
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EmployeeContext>()
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
