using WebData.Models;
namespace AppApiDapper.Services.Interface
{
    public interface ILogin
    {
        Task<UserModel> Authenticate(string username, string password);
    }
}
