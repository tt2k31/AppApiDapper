using AppApiDapper.Models;
using AppApiDapper.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AppApiDapper.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly object _options;
        private readonly IConfiguration _config;

        public UserRepository(IOptions<Appsettings> options,IConfiguration config)
        {
            _options = options.Value;
            _config = config;
        }

        public IDbConnection Connection
        {
            get 
            {   // Không lấy được config
                //return new SqlConnection(_options.ToString());


                return new SqlConnection(_config.GetConnectionString("MyDb"));

            }
        }
        public async Task Add(UserModel user)
        {
            using(IDbConnection connection = Connection)
            {
                string q = @"INSERT INTO aspnet_User (UserId, UserName,UserType) VALUES (@UserId, @UserName,@UserType)";
                connection.Open();
                //user.UserId = Guid.NewGuid();
                await connection.ExecuteAsync(q, new
                {
                    UserId = Guid.NewGuid(),
                    UserName = user.UserName,
                    UserType = user.UserType
                });
            }    
        }
       
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"SELECT * FROM aspnet_User";
                //connection.Open();
                var re = await connection.QueryAsync<AspnetUser>(q);
                return re.Select(x => new UserModel
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserType = x.UserType,
                }).ToList();
            }
        }
        public async Task<UserModel> GetById(Guid id)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"SELECT * FROM aspnet_User where UserId = @Id";
                connection.Open();
                var user = connection.Query<AspnetUser>(q, new { Id = id}).FirstOrDefault();
                if(user != null)
                {
                    return new UserModel
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        UserType = user.UserType,
                    };
                }
                return null;
            }
        }
        public async Task Delete(Guid id)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"DELETE FROM aspnet_User where UserId = @Id";
                connection.Open();
                await connection.ExecuteAsync(q, new { Id = id });
            }
        }
        public async Task Update(UserModel user)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"UPDATE aspnet_User SET UserName = @UserName, UserType = @UserType
                            where UserId = @Id";
                connection.Open();
                await connection.ExecuteAsync(q,new
                {
                    UserName = user.UserName,
                    UserType = user.UserType,
                    Id = user.UserId
                });
            }
        }

        public void Update1(UserModel user)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"UPDATE aspnet_User SET UserName = '" + user.UserName
                            + "' , UserType = " + user.UserType +
                            "where UserId = '" + user.UserId + "'";
                connection.Open();
                connection.Execute(q);
            }
        }
    }   
}
