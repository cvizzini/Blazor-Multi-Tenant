using ExampleApp.Server.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;


namespace ExampleApp.Server.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(Tenant2Attribute))]
    [Route("api/[controller]")]
    public class TenantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName
            // For ASP.NET Core >= 5.0
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
            var tenant = RouteData.Values.FirstOrDefault(r => r.Key == "tenant");
            return Ok(tenant);
        }
    }
}
