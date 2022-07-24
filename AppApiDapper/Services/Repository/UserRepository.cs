using AppApiDapper.Services.Interface;
using Dapper;
using System.Data;
using WebData.Entities;
using WebData.Models;

namespace AppApiDapper.Services.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {            
        }

        public async Task Add(UserModel entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            //string q = @"INSERT INTO aspnet_User (UserId, UserName,UserType) 
            //                VALUES (@UserId, @UserName,@UserType)";password = @password
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
            string q = @"exec usp_getUser";
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
            string q = @"exec usp_deleteUser @Id";
            await Connection.ExecuteAsync(q,
                param: new { Id = id },
                transaction: Transaction);
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
        public async Task<IEnumerable<UserModel>> GetAll(int index)
        {
            string q = @"exec usp_getUser";
            var rs = await Connection.QueryAsync<AspnetUser>(q,
                            transaction: Transaction);
            
            return rs.Select(x => new UserModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserType = x.UserType,
                password = x.password
            }).ToList().Skip((index - 1) * 2).Take(2);
        }
    }


}
