using System.Security.Claims;
using Lumina.Domain.Auth.DTOs;
using Lumina.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;
    // private readonly IEnumConverterService _enumConverterService;
    // private readonly ISummaryRepository _summaryRepository;
    // private readonly IBalanceRepository _balanceRepository;

    public AuthController(
        IAuthRepository authRepository
    //  IEnumConverterService enumConverterService,
    //   ISummaryRepository summaryRepository,
    //    IBalanceRepository balanceRepository
       )
    {
        _authRepository = authRepository;
        // _enumConverterService = enumConverterService;
        // _summaryRepository = summaryRepository;
        // _balanceRepository = balanceRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authRepository.RegisterUser(registerDto);

        if (!result.Item1)
            return BadRequest("Falha no registro do usuário");

        return Ok("Usuário registrado com sucesso");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var result = await _authRepository.Login(loginDto);
        if (result == null)
        {
            return Unauthorized("Usuário ou senha inválidos");
        }

        return Ok(result);
    }

    // [HttpPost("admin/login")]
    // public async Task<IActionResult> AdminLogin([FromBody] LoginRequestDto loginDto)
    // {
    //     var result = await _authRepository.AdminLogin(loginDto);
    //     if (result == null)
    //     {
    //         return Unauthorized("Administrador ou senha inválidos");
    //     }

    //     return Ok(result);
    // }

    // [Authorize]
    // [HttpPost("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     if (string.IsNullOrEmpty(userId))
    //     {
    //         return BadRequest("Usuário não identificado");
    //     }

    //     var result = await _authRepository.Logout(userId);
    //     if (result)
    //     {
    //         return Ok("Logout realizado com sucesso");
    //     }
    //     return BadRequest("Falha ao realizar logout");
    // }
}