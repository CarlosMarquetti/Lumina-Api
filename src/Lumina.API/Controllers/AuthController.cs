using Lumina.Domain.Auth.DTOs;
using Lumina.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, id) = await _authRepository.RegisterClient(dto, ct);
            if (!success) return BadRequest("E-mail ou usuário já existe.");

            return Ok(new { Message = "Cliente registrado com sucesso!", Id = id });
        }

        [HttpPost("register/designer")]
        public async Task<IActionResult> RegisterDesigner([FromBody] RegisterDesignerDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, id) = await _authRepository.RegisterDesigner(dto, ct);
            if (!success) return BadRequest("E-mail ou usuário já existe.");

            return Ok(new { Message = "Designer registrado com sucesso!", Id = id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
        {
            var result = await _authRepository.Login(dto, ct);
            if (result == null) return Unauthorized("Usuário ou senha inválidos.");

            return Ok(result);
        }
    }
}
