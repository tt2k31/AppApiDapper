using System.ComponentModel.DataAnnotations;

namespace WebData.Entities
{
    public class ManagerList
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public Guid OrganizationId { get; set; }

        public AspnetUser AspnetUser { get; set; }
        public AspnetOrganization AspnetOrganization { get; set; }

    }
}
