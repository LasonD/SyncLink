using System.ComponentModel.DataAnnotations;

namespace SyncLink.Server.Dtos;

public record LoginDto(
    [Required] string UsernameOrEmail,
    [Required] string Password
);