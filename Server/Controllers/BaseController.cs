using ExampleApp.Context.Repository;
using ExampleApp.Server.Interfaces;
using ExampleApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleApp.Server.Controllers
{
    public abstract class BaseController<TEntity> : ControllerBase where TEntity : BaseModel
    {
        private readonly IRepository<TEntity> _repository;

        public BaseController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity announcement)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Insert(announcement);
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
        public async Task<IActionResult> Edit([FromBody] TEntity announcement)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Update(announcement);
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
