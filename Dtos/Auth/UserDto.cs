namespace Smartspend.Api.Dtos.Auth;

public record UserDto(
    int Id,
    String FirstName,
    String? LastName,
    String Email
);
