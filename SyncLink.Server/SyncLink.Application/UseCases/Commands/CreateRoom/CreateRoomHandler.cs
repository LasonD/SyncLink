using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.CreateRoom;

public partial class CreateRoom
{
    public record Handler : IRequestHandler<Command, RoomDto>
    {


        public Task<RoomDto> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}