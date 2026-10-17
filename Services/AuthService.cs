using Microsoft.Extensions.Configuration;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Auth;
using SmartSpend.Api.Interfaces;
using Smartspend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Smartspend.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbcontext;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbcontext = dbContext;
        _configuration = configuration;
    } 

    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        var email = await  _dbcontext.Set<User>().AnyAsync(r => r.Email == dto.Email);
      if(email)  
    }

    public async Task<UserDto> LoginAsync(LoginDto dto)
    {
        
    }
}
