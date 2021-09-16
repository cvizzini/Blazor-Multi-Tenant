using ExampleApp.Context.Repository;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : BaseController<Announcement>
    {
        public AnnouncementController(IRepository<Announcement> repository) : base(repository)
        {

        }
    }
}
