namespace Smartspend.Api.Dtos.Auth;

public record UserDto(
    int Id,
    string FirstName,
    string? LastName,
    string Email,
    string Token
);
