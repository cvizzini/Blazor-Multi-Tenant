using ExampleApp.Shared;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.Server.Controllers
{
    [Authorize]
    //[ServiceFilter(typeof(UserTenantAccess2Attribute))]
    [Route("api/[controller]")]
    public class UserTenantAccessController : ControllerBase
    {
        private readonly IUserTenantRepository _repository;

        public UserTenantAccessController(IUserTenantRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("tenant/{tenantId}")]
        public async Task<IActionResult> GetAllByTenant(int tenantId)
        {
            var result = await _repository.Filter(x => x.TenantId == tenantId, new string[] { "Tenant", "User"});
            return Ok(result.Select(x=> new UserAccessDTO(x)).ToList());
        }

        [HttpGet]
        [Route("excludeTenant/{tenantId}")]
        public async Task<IActionResult> GetAllByExcludeTenant(int tenantId)
        {
            var result = await _repository.ExcludeTenant(tenantId);
            return Ok(result);
        }

        [HttpPost]
        [Route("tenant/{tenantId}/user/{userId}")]
        public async Task<IActionResult> AddUserToTenant(int tenantId, string userId)
        {
            var existing = await _repository.Filter(x => x.TenantId == tenantId && x.UserId == userId);
            if (!existing.Any())
            {
                var uta = new UserTenantAccess() { UserId = userId, TenantId = tenantId };
                var result = await _repository.Insert(uta);
                return Ok(result);
            }
            return Ok(existing);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserTenantAccess userTenantAccess)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Insert(userTenantAccess);
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
        public async Task<IActionResult> Edit([FromBody] UserTenantAccess userTenantAccess)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Update(userTenantAccess);
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
