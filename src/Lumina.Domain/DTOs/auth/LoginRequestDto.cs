using System.ComponentModel.DataAnnotations;

namespace Lumina.Domain.Auth.DTOs
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "O Email ou Usuário é obrigatório.")]
        public string UsernameOrEmail { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatório.")]
        public string Password { get; set; } = null!;
    }
}