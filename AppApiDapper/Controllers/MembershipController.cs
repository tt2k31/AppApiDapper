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
        public IActionResult Get()
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = uow.MembershipRepository.GetAll();
                    
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
        public IActionResult Get(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = uow.MembershipRepository.Get(id);
                    
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
        public IActionResult Post(MembershipModel model)
        {
            try
            {
            using (var uow = new UnitOfWork(_config))
                {
                    uow.MembershipRepository.Add(model);
                    uow.Commit();
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
        public IActionResult Put(Guid id, MembershipModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    uow.MembershipRepository.Update(model);
                    uow.Commit();
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
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    uow.MembershipRepository.Delete(id);
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
