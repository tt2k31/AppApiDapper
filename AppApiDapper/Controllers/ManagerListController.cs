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
        public IActionResult Get()
        {
            try
            {
                using (var uow = new UnitOfWork(_config.GetConnectionString("mydb")))
                {
                    var ds = uow.ManagerListRepository.GetAll();
                    uow.Commit();
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
        public IActionResult GetByUserId(Guid Id)
        {
            try
            {
                using(var uow = new UnitOfWork(_config.GetConnectionString("Mydb")))
                {
                    var ds = uow.ManagerListRepository.GetId(Id);
                    uow.Commit();
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
        public IActionResult Post(ManagerListModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(_config.GetConnectionString("Mydb")))
                {
                    uow.ManagerListRepository.Create(model);
                    uow.Commit();
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
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var uow = new UnitOfWork(_config.GetConnectionString("Mydb")))
                {
                    uow.ManagerListRepository.Delete(id);
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
