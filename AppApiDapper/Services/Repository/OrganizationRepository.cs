using AppApiDapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AppApiDapper.Services
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IConfiguration _config;

        public OrganizationRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection db
        { get
            {
                return new SqlConnection(_config.GetConnectionString("MyDb"));
            }
        }

        public void Add(OrganizationModel model)
        {
            using(IDbConnection connection = db)
            {
                string q = "INSERT INTO aspnet_Organization (OrganizationId, OrganizationCode, OrganizationName, PhoneNumber, Description, Status)" +
                    "VALUES (@OrganizationId, @OrganizationCode, @OrganizationName, @PhoneNumber, @Description, @Status)";
                connection.Open();
                connection.Execute(q, new
                {
                    OrganizationId = Guid.NewGuid(),
                    OrganizationCode = model.OrganizationCode,
                    OrganizationName = model.OrganizationName,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Status = model.Status,
                });
            }
        }

        public void Delete(Guid id)
        {
            using(IDbConnection connection = db)
            {
                string q = "DELETE FROM aspnet_Organization WHERE OrganizationId = @OrganizationId";
                connection.Open();
                connection.Execute(q, new { OrganizationId = id });
            }
        }

        public List<OrganizationModel> Get()
        {
            using (IDbConnection connection = db)
            {
                string q = "SELECT * FROM aspnet_Organization";
                connection.Open();
                return connection.Query<AspnetOrganization>(q).Select(x => new OrganizationModel
                {
                    OrganizationId = x.OrganizationId,
                    OrganizationCode = x.OrganizationCode,
                    OrganizationName = x.OrganizationName,
                    PhoneNumber = x.PhoneNumber,
                    Description = x.Description,
                    Status = x.Status,
                }).ToList();
            }
        }

        public OrganizationModel GetById(Guid id)
        {
            using(IDbConnection connection = db)
            {
                string q = "SELECT * FROM aspnet_Organization WHERE OrganizationId = @OrganizationId";
                connection.Open();
                var rs = connection.Query<AspnetOrganization>(q, new { OrganizationId = id }).FirstOrDefault();
                if(rs == null)
                {
                    return null;
                }
                return new OrganizationModel
                {
                    OrganizationId = rs.OrganizationId,
                    OrganizationCode = rs.OrganizationCode,
                    OrganizationName = rs.OrganizationName,
                    PhoneNumber = rs.PhoneNumber,
                    Description = rs.Description,
                    Status = rs.Status,
                };
            }
        }

        public void Update(OrganizationModel model)
        {
            using(IDbConnection connection = db)
            {
                //OrganizationId, OrganizationCode, OrganizationName, PhoneNumber, Description, Status
                string q = @"UPDATE aspnet_Organization SET OrganizationCode = @OrganizationCode, 
                                OrganizationName = @OrganizationName, 
                                PhoneNumber = @PhoneNumber,
                                Description = @Description,
                                Status = @Status
                                where OrganizationId = @OrganizationId";
                connection.Open();
                connection.Execute(q, new
                {
                    OrganizationId = model.OrganizationId,
                    OrganizationCode = model.OrganizationCode,
                    OrganizationName = model.OrganizationName,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Status = model.Status,
                });
            }    
        }
    }
}
