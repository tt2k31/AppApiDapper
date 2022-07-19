using AppApiDapper.Models;
using WebData.Models;

namespace AppApiDapper.Services
{
    public interface IOrganizationRepository
    {
        void Add(OrganizationModel model);
        void Update(OrganizationModel model);
        List<OrganizationModel> Get();
        OrganizationModel GetById(Guid id);
        void Delete(Guid id);
    }
}
