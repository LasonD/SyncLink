using System.ComponentModel.DataAnnotations;

namespace SyncLink.Server.Dtos;

public record RegistrationDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string Username,
    [Required] string Password,
    string? Email
);