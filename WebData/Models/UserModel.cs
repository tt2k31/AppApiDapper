using System.ComponentModel.DataAnnotations;

namespace WebData.Models
{
    public class UserModel
    {
        
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int? UserType { get; set; }

    }
}
