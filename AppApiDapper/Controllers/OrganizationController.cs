using WebData.Models;
using AppApiDapper.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public OrganizationController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/<OrganizationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.OrganizationRepository.All();
                    uow.Commit();
                    return Ok(ds);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<OrganizationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.OrganizationRepository.GetById(id);
                    uow.Commit();
                    return Ok(ds);
                }
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<OrganizationController>
        [HttpPost]
        public async Task<IActionResult> Post(OrganizationModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.OrganizationRepository.Add(model);
                    uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<OrganizationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, OrganizationModel model)
        {  
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.OrganizationRepository.Update(model);
                    uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<OrganizationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.OrganizationRepository.Delete(id);
                    uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
