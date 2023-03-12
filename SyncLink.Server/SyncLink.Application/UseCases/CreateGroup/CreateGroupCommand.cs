using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.CreateGroup;

public partial class CreateGroup
{
    public record Command(int UserId, string Name, string Description) : IRequest<GroupDto>;
}

