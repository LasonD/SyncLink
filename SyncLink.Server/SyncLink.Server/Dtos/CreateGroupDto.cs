using System.ComponentModel.DataAnnotations;

namespace SyncLink.Server.Dtos;

public record CreateGroupDto(
    [Required, MaxLength(50)] string Name,
    string? Description
);