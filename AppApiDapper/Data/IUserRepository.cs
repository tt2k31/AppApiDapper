using AppApiDapper.Models;

namespace AppApiDapper.Data
{
    public interface IUserRepository
    {
        void Add(UserModel user);
        List<UserModel> GetAll();
        UserModel GetById(Guid id);
        void Delete(Guid id);
        void Update(UserModel user);
    }
}
