using AppApiDapper.Models;
using AppApiDapper.Services.Interface;
using Dapper;
using System.Data;
using WebData.Entities;
using WebData.Models;

namespace AppApiDapper.Services.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly MyDBContext _context;
        public UserRepository(IDbTransaction transaction, MyDBContext context) : base(transaction)
        {            
            _context = context;
        }

        public async Task Add(UserModel entity)
        {
            if (entity == null)
            {                                
            }
            
            string q = @"exec usp_userAdd @UserId, @UserName,@UserType,@password";
            await Connection.ExecuteAsync(q, 
                param: new
                {
                    UserId = Guid.NewGuid(),
                    UserName = entity.UserName,
                    UserType = entity.UserType,
                    password = entity.password
                },
                transaction: Transaction
                );
        }

        public async Task<IEnumerable<UserModel>> All()
        {
            string q = @"exec usp_getAllUser ";
            var rs = await Connection.QueryAsync<AspnetUser>(q,
                            transaction: Transaction);
            return rs.Select( x => new UserModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserType = x.UserType,
                password = x.password
            }).ToList();
        }

        public async Task Delete(Guid id)
        {
            //string q = @"exec usp_deleteUser @Id";
            //await Connection.ExecuteAsync(q,
            //    param: new { Id = id },
            //    transaction: Transaction);
            var user = _context.AspnetUsers.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {                
            }
            _context.AspnetUsers.Remove(user);
        }

        public async Task<UserModel> GetById(Guid id)
        {
            string q = @"exec usp_getUserID @Id";
            var rs = await Connection.QueryAsync<AspnetUser>(q,
                param: new 
                {
                    Id = id
                },
                transaction: Transaction
                );
            return rs.Select(x => new UserModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserType = x.UserType,
                password = x.password
            }).FirstOrDefault();
        }

        public async Task Update(UserModel entity)
        {
            string q = @"exec usp_userUpdate @UserId, @UserName,@UserType,@password ";
            await Connection.ExecuteAsync(q,
                param: new
                {
                    UserId = entity.UserId,
                    UserName = entity.UserName,
                    UserType = entity.UserType,
                    password = entity.password
                },
                transaction: Transaction
                );
        }
        public async Task<IEnumerable<UserModel>> GetAll(int pageIndex, int pageSize)
        {
            string q = @"exec usp_getUser @pageIndex, @pageSize";
            var rs = await Connection.QueryAsync<AspnetUser>(q,
                            param: new
                            {
                                pageIndex = pageIndex,
                                pageSize = pageSize
                            },
                            transaction: Transaction);
            
            return rs.Select(x => new UserModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserType = x.UserType,
                password = x.password
            }).ToList();
        }

        public async Task<UserModel> GetByName(string name)
        {
            var user = _context.AspnetUsers.FirstOrDefault(u => u.UserName == name);
            if(user == null)
            {
                return null;
            }    
            else
            {
                return new UserModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserType = user.UserType,
                    password = user.password
                };
            }    
        }
    }


}
