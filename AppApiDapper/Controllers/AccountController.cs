using AppApiDapper.Models;
using AppApiDapper.Services;
using AppApiDapper.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebData.Models;
using WebData.Entities;
using AppApiDapper.Services;
using Microsoft.Extensions.Options;

namespace AppApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Appsettings _appsetting;
        private readonly MyDBContext _context;
        private ILogin _login;
        public AccountController(
          IOptions<Appsettings> appsetting,
         MyDBContext context,
         ILogin login)
        {
            _appsetting = appsetting.Value;
            _context = context;
            _login = login;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var rs = await _context.AspnetUsers.FirstOrDefaultAsync(x =>
                    x.UserName == model.UserName && x.password == model.password);
            if (rs == null)
            {
                return Unauthorized();
            }
            var user = await _login.Authenticate(model.UserName, model.password);
            if(user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginResModel();
            loginRes.UserName = model.UserName;
            loginRes.token = await GenToken(rs);
            return Ok(loginRes);
        }
        private async Task<string> GenToken(AspnetUser model)
        {
            var JWtTokenHander = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(_appsetting.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.UserName ),  
                    new Claim("UserType",model.UserType.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),                                        
                    //role                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                                (secretKeyByte), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = JWtTokenHander.CreateToken(tokenDescription);

            var accessToken = JWtTokenHander.WriteToken(token);
            return accessToken;
        }
    }
}
