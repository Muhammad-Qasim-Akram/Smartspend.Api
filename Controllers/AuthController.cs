using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Auth;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private const string AuthCookieName = "smartspend_auth";
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
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);

        Response.Cookies.Append(AuthCookieName, result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/",
            Expires = new DateTimeOffset(jwt.ValidTo, TimeSpan.Zero)
        });

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<ProfileDto> Me()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email is null)
            return Unauthorized();

        return Ok(new ProfileDto(
            email,
            User.FindFirstValue(ClaimTypes.GivenName)));
    }
}