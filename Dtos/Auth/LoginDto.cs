using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Auth;

public record LoginDto(
    [Required][EmailAddress] string Email,
    [Required][MinLength(8)] string Password
);
