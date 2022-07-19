namespace WebData.Models
{
    public class MembershipModel
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Status { get; set; }
    }
}
