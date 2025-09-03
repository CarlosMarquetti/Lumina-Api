using Lumina.Domain.Auth.DTOs;
// using Lumina.Domain.Entities;
// using Lumina.Domain.Users.DTOs;
using Lumina.Domain.Repository.Interfaces;

namespace Lumina.Domain.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<(bool, int)> RegisterClient(RegisterClientDto dto, CancellationToken cancellationToken = default);
        Task<(bool, int)> RegisterDesigner(RegisterDesignerDto dto, CancellationToken cancellationToken = default);

        Task<LoginResponseDto?> Login(LoginRequestDto dto, CancellationToken cancellationToken = default);
    }
}