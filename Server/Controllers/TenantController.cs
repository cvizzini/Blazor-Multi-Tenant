using ExampleApp.Server.Filter;
using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using ExampleApp.TenantContext.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExampleApp.Server.Controllers
{
    [Authorize]
    //[ServiceFilter(typeof(Tenant2Attribute))]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _repository;

        public TenantController(ITenantRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName
            // For ASP.NET Core >= 5.0
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
            var tenant = RouteData.Values.FirstOrDefault(r => r.Key == "tenant");
            var result = await _repository.GetAll();
            return Ok(result);
        } 
        
        
        [HttpPatch]
        public IActionResult Test()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName
            // For ASP.NET Core >= 5.0
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
            var tenant = RouteData.Values.FirstOrDefault(r => r.Key == "tenant");
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Insert(tenant);
                return Ok(result);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {


            var result = await _repository.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Update(tenant);
                return Ok(result);
            }

            return BadRequest(ModelState.Values);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
