using AppApiDapper.Services.Interface;
using WebData.Entities;
using WebData.Models;
using Dapper;
using System.Data;

namespace AppApiDapper.Services.Repository
{
    public class MembershipRepository : RepositoryBase, IMembershipRepository
    {
        public MembershipRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task Add(MembershipModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            string q = @"exec  usp_MembershipAdd @UserId, @FullName, @Address, @PhoneNumber, @Email, @Status";
            await Connection.ExecuteAsync(q,
                param: new
                {
                    UserId = model.UserId,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Status = model.Status
                },
                transaction: Transaction
                );                
        }

        public async Task Delete(Guid id)
        {
            string q = "exec usp_MembershipDelete @FindId";
            await Connection.ExecuteAsync(q,
                param: new { FindId = id },
                transaction: Transaction
                );
        }

        public async Task<MembershipModel> GetById(Guid id)
        {
            string q = "exec usp_MembershipGetById @FindId";
            var rs = await Connection.QueryAsync<MembershipModel>(q,
                    param: new { FindId = id },
                    transaction: Transaction
                );
            return rs.Select(x => new MembershipModel
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Status = x.Status
                }).FirstOrDefault();
        }

        public async Task<IEnumerable<MembershipModel>> All()
        {
            string q = "exec usp_MembershipGetAll";
            var rs = await Connection.QueryAsync<MembershipModel>(q,
                transaction: Transaction
                );
            return rs.Select(x => new MembershipModel
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Status = x.Status
                }).ToList();
        }

        public async Task Update(MembershipModel model)
        {
            string q = @"exec  usp_MembershipUpdate @UserId, @FullName, @Address, @PhoneNumber, @Email, @Status";
            await Connection.ExecuteAsync(q,
                param: new
                {
                    UserId = model.UserId,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Status = model.Status
                },
                 transaction: Transaction
                 );
        }
    }
}
