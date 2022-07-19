using AppApiDapper.Data;
using AppApiDapper.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var rs = _repository.GetById(id);
                if(rs == null)
                {
                    return NotFound();
                }
                return Ok(rs);
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post(UserModel model)
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

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UserModel model)
        {
            if(id != model.UserId)
            {
                return NotFound();
            }    
            try
            {
                _repository.Update(model);
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<UserController>/5
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
