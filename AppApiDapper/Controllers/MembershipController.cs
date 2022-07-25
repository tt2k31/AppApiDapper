using WebData.Models;
using AppApiDapper.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    { 
        private readonly IConfiguration _config;

        public MembershipController(IConfiguration config)
        {
            _config = config;
        }
        

        // GET: api/<MembershipController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.MembershipRepository.All();                    
                    return Ok(ds);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<MembershipController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.MembershipRepository.GetById(id);                    
                    return Ok(ds);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<MembershipController>
        [HttpPost]
        public async Task<IActionResult> Post(MembershipModel model)
        {
            try
            {
            using (var uow = new UnitOfWork(_config))
                {
                    await uow.MembershipRepository.Add(model);
                    await uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<MembershipController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, MembershipModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.MembershipRepository.Update(model);
                    await uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<MembershipController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.MembershipRepository.Delete(id);
                    await uow.Commit();
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
