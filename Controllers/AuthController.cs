using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Auth;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
    {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto dto)
    {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
    }
}