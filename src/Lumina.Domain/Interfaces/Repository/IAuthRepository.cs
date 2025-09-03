using Lumina.Domain.Auth.DTOs;
// using Lumina.Domain.Entities;
// using Lumina.Domain.Users.DTOs;
using Lumina.Domain.Repository.Interfaces;

namespace Lumina.Domain.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<(bool, int)> RegisterUser(RegisterRequestDto registerDto, CancellationToken cancellationToken = default);
        Task<LoginResponseDto> Login(LoginRequestDto loginDto, CancellationToken cancellationToken = default);
        // Task<LoginResponseDto> AdminLogin(LoginRequestDto loginDto, CancellationToken cancellationToken = default);
        // Task<bool> Logout(string userId);
    }
}