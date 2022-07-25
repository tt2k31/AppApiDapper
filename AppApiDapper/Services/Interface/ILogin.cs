using WebData.Models;
namespace AppApiDapper.Services.Interface
{
    public interface ILogin
    {
        Task<LoginModel> Authenticate(string username, string password);
    }
}
