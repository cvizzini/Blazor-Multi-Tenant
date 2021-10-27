using ExampleApp.Context.Repository;
using ExampleApp.Server.Filter;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleApp.Server.Controllers
{
    [Authorize]
   [ServiceFilter(typeof(Tenant2Attribute))]
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : BaseController<Announcement>
    {
        public AnnouncementController(IRepository<Announcement> repository) : base(repository)
        {

        }
    }
}
