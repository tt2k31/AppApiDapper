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
        public void Add(UserModel user)
        {
            using(IDbConnection connection = Connection)
            {
                string q = @"INSERT INTO aspnet_User (UserId, UserName,UserType) VALUES (@UserId, @UserName,@UserType)";
                connection.Open();
                //user.UserId = Guid.NewGuid();
                connection.Execute(q, new
                {
                    UserId = Guid.NewGuid(),
                    UserName = user.UserName,
                    UserType = user.UserType
                });
            }    
        }
       
        public List<UserModel> GetAll()
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"SELECT * FROM aspnet_User";
                //connection.Open();
                return connection.Query<AspnetUser>(q).Select(x => new UserModel
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserType = x.UserType,
                }).ToList();
            }
        }
        public UserModel GetById(Guid id)
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
        public void Delete(Guid id)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"DELETE FROM aspnet_User where UserId = @Id";
                connection.Open();
                connection.Execute(q, new { Id = id });
            }
        }
        public void Update(UserModel user)
        {
            using (IDbConnection connection = Connection)
            {
                string q = @"UPDATE aspnet_User SET UserName = @UserName, UserType = @UserType
                            where UserId = @Id";
                connection.Open();
                connection.Execute(q,new
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
