using System.ComponentModel.DataAnnotations;

namespace Lumina.Domain.Auth.DTOs
{
    public class BaseRegisterDto
    {
        [Required(ErrorMessage = "O Usuário é obrigatório.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "O CpfCnpj é obrigatório.")]
        public string CpfCnpj { get; set; } = null!;

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        public string PhoneNumber { get; set; } = null!;
    }

    public class RegisterClientDto : BaseRegisterDto
    {
        // nada extra por enquanto
    }

    public class RegisterDesignerDto : BaseRegisterDto
    {
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string State { get; set; } = null!;

        [Required(ErrorMessage = "O cep é obrigatório.")]
        public string ZipCode { get; set; } = null!;

        public string? Complement { get; set; }
    }
}
