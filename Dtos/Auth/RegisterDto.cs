using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Auth;

public record RegisterDto
(
    [Required][StringLength(50)] string FirstName,
    [StringLength(50)] string? LastName,
    [Required][EmailAddress] string Email,
    [Required][MinLength(8)] string Password
);
