using WebData.Models;

namespace AppApiDapper.Services.Repository
{
    public interface IMembershipRepository
    {
        void Add(MembershipModel model);
        MembershipModel Get(Guid id);
        List<MembershipModel> GetAll();
        void Update(MembershipModel model);
        void Delete(Guid id);
    }
}
