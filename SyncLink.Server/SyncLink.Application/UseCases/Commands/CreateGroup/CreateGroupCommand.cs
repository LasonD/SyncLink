using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.CreateGroup;

public partial class CreateGroup
{
    public record Command : IRequest<GroupOverviewDto>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}

