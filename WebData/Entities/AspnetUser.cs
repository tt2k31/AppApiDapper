using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebData.Entities
{
    public class AspnetUser
    {
        public AspnetUser()
        {
            ManagerLists = new HashSet<ManagerList>();
        }
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int? UserType { get; set; }

        public  AspnetMembership AspnetMembership { get; set; } = null!;

        public ICollection<ManagerList> ManagerLists { get; set; }
    }
}
