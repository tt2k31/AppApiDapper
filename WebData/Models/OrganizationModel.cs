namespace WebData.Models
{
    public class OrganizationModel
    {
        public Guid OrganizationId { get; set; }
        public string? OrganizationCode { get; set; }
        public string? OrganizationName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
