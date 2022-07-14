using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppApiDapper.Models
{
    public  class AspnetMembership
    {
        [Key]
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Status { get; set; }

        public AspnetUser User { get; set; } = null!;
    }
}
