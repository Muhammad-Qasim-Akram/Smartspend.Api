namespace Smartspend.Api.Dtos.Auth;

/// <summary>Represents the currently authenticated user.</summary>
public sealed record ProfileDto(
    string Email,
    string? FirstName);
