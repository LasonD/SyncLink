using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.CreateGroup;

public partial class CreateGroup
{
    public record Command : IRequest<GroupDto>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

