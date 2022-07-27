using AppApiDapper.Models;
using AppApiDapper.Services.Repository;
using AppApiDapper.Services.Interface;
using WebData.Models;
using Microsoft.AspNetCore.Mvc;
using AppApiDapper.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        public UserController(
            IConfiguration config,
            ILogger<UserController> logger,
            MyDBContext context
            )
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        // GET: api/<UserController>v
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.UserRepository.GetAll(pageIndex, pageSize);
                    return Ok(ds);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("err userList: " + ex.Message);
                return BadRequest();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = await uow.UserRepository.GetById(id);
                    return Ok(ds);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("err userGetById: " + ex.Message);
                return BadRequest();
            }
        }

        // POST api/<UserController>

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.UserRepository.Add(model);
                    await uow.Commit();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("err userAdd: " + ex.Message);
                return BadRequest();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UserModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    await uow.UserRepository.Update(model);
                    await uow.Commit();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("err userUpdate: " + ex.Message);
                return BadRequest();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_context))
                {
                    await uow.UserRepository.Delete(id);
                    //await uow.Commit();
                    await uow.CompleteAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("err userDel: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
