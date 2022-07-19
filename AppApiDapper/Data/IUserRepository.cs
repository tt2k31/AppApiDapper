using WebData.Models;

namespace AppApiDapper.Data
{
    public interface IUserRepository
    {
        Task Add(UserModel user);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(Guid id);
        Task Delete(Guid id);
        Task Update(UserModel user);
    }
}
