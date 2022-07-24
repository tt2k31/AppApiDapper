using AppApiDapper.Models;
using AppApiDapper.Services.Interface;
using WebData.Entities;
using WebData.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AppApiDapper.Services.Repository
{
    public class OrganizationRepository : RepositoryBase, IOrganizationRepository
    {

        public OrganizationRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task Add(OrganizationModel model)
        {
            if (model == null)
            {
                throw new NotImplementedException();
            }
            string q = "exec usp_OrganizationAdd @OrganizationId, @OrganizationCode, @OrganizationName, @PhoneNumber, @Description, @Status";

            await Connection.ExecuteAsync(q,
                param: new
                {
                    OrganizationId = Guid.NewGuid(),
                    OrganizationCode = model.OrganizationCode,
                    OrganizationName = model.OrganizationName,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Status = model.Status,
                },
                transaction: Transaction);

        }

        public async Task Delete(Guid id)
        {
            string q = "exec usp_deleteOrganization @OrganizationId";

            await Connection.ExecuteAsync(q,
                param: new { OrganizationId = id },
                transaction: Transaction
                );
        }

        public async Task<IEnumerable<OrganizationModel>> All()
        {
            string q = "exec usp_getOrganization";
            var rs = await Connection.QueryAsync<AspnetOrganization>(q,
                            transaction: Transaction);
            return rs.Select(x => new OrganizationModel
            {
                OrganizationId = x.OrganizationId,
                OrganizationCode = x.OrganizationCode,
                OrganizationName = x.OrganizationName,
                PhoneNumber = x.PhoneNumber,
                Description = x.Description,
                Status = x.Status,
            }).ToList();

        }

        public async Task<OrganizationModel> GetById(Guid id)
        {
            string q = "exec usp_getOrganizationId @OrganizationId";

            var rs = await Connection.QueryAsync<AspnetOrganization>(q,
                param: new { OrganizationId = id },
                transaction: Transaction);
            if (rs == null)
            {
                return null;
            }
            return rs.Select(x => new OrganizationModel
            {
                OrganizationId = x.OrganizationId,
                OrganizationCode = x.OrganizationCode,
                OrganizationName = x.OrganizationName,
                PhoneNumber = x.PhoneNumber,
                Description = x.Description,
                Status = x.Status,
            }).FirstOrDefault();
        }
        public async Task Update(OrganizationModel model)
        {
            string q = @"exec usp_OrganizationUpdate @OrganizationId, @OrganizationCode, @OrganizationName, @PhoneNumber, @Description, @Status";

            await Connection.ExecuteAsync(q, param: new
            {
                OrganizationId = model.OrganizationId,
                OrganizationCode = model.OrganizationCode,
                OrganizationName = model.OrganizationName,
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
                Status = model.Status,
            },
            transaction: Transaction
            );
        }
    }
}

