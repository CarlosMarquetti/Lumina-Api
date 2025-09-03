namespace Lumina.Domain.Auth.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
    }
}