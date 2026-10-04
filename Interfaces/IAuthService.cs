using Smartspend.Api.Dtos.Auth;
namespace SmartSpend.Api.Interfaces;
public interface IAuthService
{
    Task<UserDto> RegisterAsync(RegisterDto dto);
    Task<UserDto> LoginAsync(LoginDto dto);
}