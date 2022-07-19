using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebData.Models
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
