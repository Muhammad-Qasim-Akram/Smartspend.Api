using Microsoft.Extensions.Configuration;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Auth;
using SmartSpend.Api.Interfaces;
using Smartspend.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace Smartspend.Api.Services;

public class AuthService : IAuthService
{
    private string GenerateToken(User user)
{
    var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!);
    
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        }),
        Expires = DateTime.UtcNow.AddDays(
            int.Parse(_configuration["JwtSettings:ExpiryDays"]!)),
        Issuer = _configuration["JwtSettings:Issuer"],
        Audience = _configuration["JwtSettings:Audience"],
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256
        )
    };

    var handler = new JwtSecurityTokenHandler();
    var token = handler.CreateToken(tokenDescriptor);
    return handler.WriteToken(token);
}
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
            throw new InvalidOperationException("Email already exists.");
      
         var hashedPw = BCrypt.Net.BCrypt.HashPassword(dto.Password);
          var user = new User
          {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = hashedPw
          };
            await  _dbcontext.Set<User>().AddAsync(user);
            await _dbcontext.SaveChangesAsync();

            var Token = GenerateToken(user);

            return new  UserDto(user.Id,user.FirstName,user.LastName,user.Email,Token);
    }

    public async Task<UserDto> LoginAsync(LoginDto dto)
    {
        
    }
}
