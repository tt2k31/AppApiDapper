using WebData.Models;
using AppApiDapper.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerListController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ManagerListController( IConfiguration config)
        {
            _config = config;
        }

        // GET: api/<ManagerListController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    var ds = uow.ManagerListRepository.GetAll();
                    await uow.Commit();
                    return Ok(ds);
                }
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // GET api/<ManagerListController>/5
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetByUserId(Guid Id)
        {
            try
            {
                using(var uow = new UnitOfWork(_config))
                {
                    var ds = uow.ManagerListRepository.GetId(Id);
                    await uow.Commit();
                    return Ok(ds);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        

        // POST api/<ManagerListController>
        [HttpPost]
        public async Task<IActionResult> Post(ManagerListModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    uow.ManagerListRepository.Create(model);
                    await uow.Commit();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ManagerListController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ManagerListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config))
                {
                    uow.ManagerListRepository.Delete(id);
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
