using AppApiDapper.Models;
using AppApiDapper.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationController(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<OrganizationController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                
                return Ok(_repository.Get());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<OrganizationController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_repository.GetById(id));
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<OrganizationController>
        [HttpPost]
        public IActionResult Post(OrganizationModel model)
        {
            try
            {
                _repository.Add(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<OrganizationController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, OrganizationModel model)
        {
            if(id != model.OrganizationId)
            {
                return NotFound();
            }    
            try
            {
                _repository.Update(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<OrganizationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
