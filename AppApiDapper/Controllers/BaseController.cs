using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.FindFirst("id").Value;
        }
    }
}
