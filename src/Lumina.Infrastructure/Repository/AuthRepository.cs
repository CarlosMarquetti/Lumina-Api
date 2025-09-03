using Lumina.Domain.Auth.DTOs;
using Lumina.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using Lumina.Infrastructure.Data;
using Lumina.Domain.Entities;

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

        public async Task<(bool, int)> RegisterUser(RegisterRequestDto registerDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingClient = await (
                    from Client in _context.Clients
                    where Client.Email == registerDto.Email || Client.Username == registerDto.Username
                    select Client
                ).AnyAsync(cancellationToken);

                if (existingClient)
                {
                    _logger.LogWarning("Falha no registro: e-mail ou nome de usuário já existe.");
                    return (false, 0);
                }

                var client = new Client
                {
                    Username = registerDto.Username,
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    PasswordHash = HashPassword(registerDto.Password),
                    CpfCnpj = registerDto.CpfCnpj,
                    PhoneNumber = registerDto.PhoneNumber,
                    // Address = registerDto.Address,
                    // City = registerDto.City,
                    // State = registerDto.State,
                    // ZipCode = registerDto.ZipCode,
                    // CreatedAt = DateTime.Today.Date
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Usuário registrado com sucesso: {Email}", registerDto.Email);
                return (true, client.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Email}", registerDto.Email);
                throw;
            }
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await (
                    from u in _context.Clients
                    where u.Email == loginDto.UsernameOrEmail || u.Username == loginDto.UsernameOrEmail
                    select u
                ).FirstOrDefaultAsync(cancellationToken);

                if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Falha ao tenta realizar o login: {UsernameOrEmail}", loginDto.UsernameOrEmail);
                    return null;
                }

                var token = _jwtService.GenerateToken(user.Id.ToString(), user.FullName);

                var response = new LoginResponseDto
                {
                    Id = user.Id,
                    Token = token,
                    FullName = user.FullName,
                };

                _logger.LogInformation("User logged in successfully: {UsernameOrEmail}", loginDto.UsernameOrEmail);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user: {UsernameOrEmail}", loginDto.UsernameOrEmail);
                throw;
            }
        }

        // public async Task<LoginResponseDto> AdminLogin(LoginRequestDto loginDto, CancellationToken cancellationToken = default)
        // {
        //     try
        //     {
        //         var admin = await (
        //             from a in _context.Admins
        //             where a.Email == loginDto.UsernameOrEmail || a.Username == loginDto.UsernameOrEmail
        //             select a
        //         ).FirstOrDefaultAsync(cancellationToken);

        //         if (admin == null || !VerifyPassword(loginDto.Password, admin.PasswordHash))
        //         {
        //             _logger.LogWarning("Failed admin login attempt for: {UsernameOrEmail}", loginDto.UsernameOrEmail);
        //             return null;
        //         }

        //         admin.LastAccessAt = DateTime.UtcNow;
        //         await _context.SaveChangesAsync(cancellationToken);

        //         var token = _jwtService.GenerateToken(admin.Id.ToString(), admin.FullName, EnRole.Admin.ToString());

        //         var response = new LoginResponseDto
        //         {
        //             Id = admin.Id,
        //             Token = token,
        //             FullName = admin.FullName,
        //             Role = EnRole.Admin.ToString()
        //         };

        //         _logger.LogInformation("Admin logged in successfully: {UsernameOrEmail}", loginDto.UsernameOrEmail);
        //         return response;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error during admin login for: {UsernameOrEmail}", loginDto.UsernameOrEmail);
        //         throw;
        //     }
        // }



        // private string GenerateUniqueAffiliateCode()
        // {
        //     const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
        //     var random = new Random();

        //     while (true)
        //     {
        //         var code = new string(Enumerable.Repeat(chars, 8)
        //             .Select(s => s[random.Next(s.Length)]).ToArray());

        //         var exists = (
        //             from u in _context.Clients
        //             where u.AffiliateCode == code
        //             select u
        //         ).Any();

        //         if (!exists)
        //         {
        //             return code;
        //         }
        //     }
        // }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[SALT_SIZE];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

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
            if (parts.Length != 2)
                return false;

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