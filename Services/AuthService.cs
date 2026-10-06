using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Auth;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbcontext;

    public AuthService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    } 

    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        
    }
}
