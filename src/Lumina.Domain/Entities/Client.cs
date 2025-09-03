namespace Lumina.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string CpfCnpj { get; set; }
        public string PhoneNumber { get; set; }
        // public string Address { get; set; }
        // public string City { get; set; }
        // public string State { get; set; }
        // public string ZipCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}