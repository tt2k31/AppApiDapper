using WebData.Models;

namespace AppApiDapper.Services.Repository
{
    public interface IManagerListRepository
    {
        void Create(ManagerListModel model);
        
        void Delete(Guid id);
        List<ManagerListModel> GetAll();
        List<ManagerListModel> GetId(Guid Id);

    }
}
