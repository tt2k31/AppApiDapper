using AppApiDapper.Models;
using Dapper;
using System.Data;

namespace AppApiDapper.Services.Repository
{
    public class MembershipRepository : RepositoryBase, IMembershipRepository
    {
        public MembershipRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(MembershipModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            string q = @"INSERT INTO aspnet_Membership 
                        (UserId, FullName, Address, PhoneNumber, Email, Status)
                        VALUES(@UserId, @FullName, @Address, @PhoneNumber, @Email, @Status)";
            Connection.Execute(q,
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

        public void Delete(Guid id)
        {
            string q = "DELETE FROM aspnet_Membership WHERE UserId = @FindId";
            Connection.Execute(q,
                param: new { FindId = id },
                transaction: Transaction
                );
        }

        public MembershipModel Get(Guid id)
        {
            string q = "SELECT * FROM aspnet_Membership WHERE UserId = @FindId";
            return Connection.Query<MembershipModel>(q,
                    param: new { FindId = id },
                    transaction: Transaction
                ).Select(x => new MembershipModel
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Status = x.Status
                }).FirstOrDefault();
        }

        public List<MembershipModel> GetAll()
        {
            string q = "SELECT * FROM aspnet_Membership";
            return Connection.Query<MembershipModel>(q,
                transaction: Transaction
                ).Select(x => new MembershipModel
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Status = x.Status
                }).ToList();
        }

        public void Update(MembershipModel model)
        {
            string q = @"UPDATE aspnet_Membership SET
                    FullName = @FullName, Address = @Address, 
                    PhoneNumber = @PhoneNumber, Email = @Email, Status = @Status
                    WHERE UserId = @UserId";
            Connection.Execute(q,
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
