using System;
using System.Collections.Generic;

namespace WebData.Entities
{
    public class AspnetOrganization
    {
        public AspnetOrganization()
        {
            ManagerLists = new HashSet<ManagerList>();
        }

        public Guid OrganizationId { get; set; }
        public string? OrganizationCode { get; set; }
        public string? OrganizationName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }

        public ICollection<ManagerList> ManagerLists { get; set; }
    }
}
