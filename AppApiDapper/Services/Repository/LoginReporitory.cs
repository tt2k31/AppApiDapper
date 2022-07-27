using AppApiDapper.Models;
using AppApiDapper.Services.Interface;
using Microsoft.EntityFrameworkCore;
using WebData.Models;

namespace AppApiDapper.Services.Repository
{
    public class LoginReporitory : ILogin
    {
        private MyDBContext _context;

        public LoginReporitory(MyDBContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Authenticate(string username, string password)
        {
            var rs = await _context.AspnetUsers.FirstOrDefaultAsync(x => 
                        x.UserName ==username && x.password ==password);
            if(rs != null)
            {
                return new UserModel
                {
                    UserId = rs.UserId,
                    UserName = username,
                    password = password,
                    UserType = rs.UserType
                };
            } else
            {
                return null;
            }
            
            
        }
    }
}
