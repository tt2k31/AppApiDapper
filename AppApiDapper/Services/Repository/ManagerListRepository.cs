using AppApiDapper.Models;
using Dapper;
using System.Data;

namespace AppApiDapper.Services.Repository
{
    public class ManagerListRepository : RepositoryBase, IManagerListRepository
    {
        public ManagerListRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Create(ManagerListModel model)
        {
            if(model == null) throw new ArgumentNullException("model");
            Connection.Execute
                (
                    "INSERT INTO aspnet_ManagerList (UserId, OrganizationId) VALUES(@UserId, @OrganizationId)",
                    param: new { UserId = model.UserId, OrganizationId = model.OrganizationId },
                    transaction: Transaction
                );
        }

        public void Delete(Guid id)
        {
            Connection.Execute
                (
                    "DELETE FROM aspnet_ManagerList WHERE UserId = @FindId " +
                    "OR OrganizationId = @FindId",
                    param: new { FindId = id },
                    transaction: Transaction
                );
        }

        public List<ManagerListModel> GetAll()
        {
            return Connection.Query<ManagerListModel>(
                "SELECT * FROM aspnet_ManagerList",
                 transaction: Transaction
                ).Select(x => new ManagerListModel
                {
                    UserId = x.UserId,
                    OrganizationId = x.OrganizationId
                }).ToList();
        }

        public List<ManagerListModel> GetId(Guid Id)
        {
            return Connection.Query<ManagerListModel>
                (
                    "SELECT * FROM aspnet_ManagerList WHERE UserId = @FindId " +
                    "OR OrganizationId = @FindId",
                    param: new { FindId = Id },
                    transaction: Transaction
                ).Select(x => new ManagerListModel
                {
                    UserId = x.UserId,
                    OrganizationId = x.OrganizationId
                }).ToList();
        }

        
    }
}
