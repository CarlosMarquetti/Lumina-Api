using Lumina.Domain.Auth.DTOs;
using Lumina.Domain.Entities;
using Lumina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Lumina.Domain.Repository.Interfaces;

namespace Lumina.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LuminaDbContext _context;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthRepository> _logger;

        private const int ITERATION_COUNT = 10000;
        private const int SALT_SIZE = 128 / 8;
        private const int HASH_SIZE = 256 / 8;

        public AuthRepository(LuminaDbContext context, JwtService jwtService, ILogger<AuthRepository> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<(bool, int)> RegisterClient(RegisterClientDto dto, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Clients
                .AnyAsync(c => c.Email == dto.Email || c.Username == dto.Username, cancellationToken);

            if (exists) return (false, 0);

            var client = new Client
            {
                Username = dto.Username,
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                CpfCnpj = dto.CpfCnpj,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync(cancellationToken);

            return (true, client.Id);
        }

        public async Task<(bool, int)> RegisterDesigner(RegisterDesignerDto dto, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Designers
                .AnyAsync(d => d.Email == dto.Email || d.Username == dto.Username, cancellationToken);

            if (exists) return (false, 0);

            var designer = new Designer
            {
                Username = dto.Username,
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                CpfCnpj = dto.CpfCnpj,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CreatedAt = DateTime.UtcNow
            };

            _context.Designers.Add(designer);
            await _context.SaveChangesAsync(cancellationToken);

            return (true, designer.Id);
        }

        public async Task<LoginResponseDto?> Login(LoginRequestDto dto, CancellationToken cancellationToken = default)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == dto.UsernameOrEmail || c.Username == dto.UsernameOrEmail, cancellationToken);

            if (client != null && VerifyPassword(dto.Password, client.PasswordHash))
            {
                var token = _jwtService.GenerateToken(client.Id.ToString(), client.FullName, "Client");
                return new LoginResponseDto { Id = client.Id, Token = token, FullName = client.FullName };
            }

            var designer = await _context.Designers
                .FirstOrDefaultAsync(d => d.Email == dto.UsernameOrEmail || d.Username == dto.UsernameOrEmail, cancellationToken);

            if (designer != null && VerifyPassword(dto.Password, designer.PasswordHash))
            {
                var token = _jwtService.GenerateToken(designer.Id.ToString(), designer.FullName, "Designer");
                return new LoginResponseDto { Id = designer.Id, Token = token, FullName = designer.FullName };
            }

            return null;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[SALT_SIZE];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: ITERATION_COUNT,
                numBytesRequested: HASH_SIZE));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: ITERATION_COUNT,
                numBytesRequested: HASH_SIZE));

            return hash == hashed;
        }
    }
}
