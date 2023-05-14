using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Commands.SendMessage;

public partial class SendMessage
{
    public class Handler : IRequestHandler<Command, MessageDto>
    {
        private readonly IMapper _mapper;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessagesRepository _messageRepository;
        private readonly INotificationsService _notificationsService;

        public Handler(IMapper mapper, IRoomsRepository roomsRepository, IUserRepository userRepository, IMessagesRepository messageRepository, INotificationsService notificationsService)
        {
            _mapper = mapper;
            _roomsRepository = roomsRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _notificationsService = notificationsService;
        }

        public async Task<MessageDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var senderResult = await _userRepository.GetUsersFromGroupAsync(request.GroupId, new[] { request.SenderId }, cancellationToken);

            var sender = senderResult.GetResult().Entities.Single();

            var room = await ResolveRoomAsync(request, sender, cancellationToken);

            var message = new Message(sender, room!, request.Text);

            await _messageRepository.CreateAsync(message, cancellationToken);

            await _roomsRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<MessageDto>(message);

            await _notificationsService.NotifyMessageReceivedAsync(request.GroupId, dto, cancellationToken);

            return dto;
        }

        private async Task<Room?> ResolveRoomAsync(Command request, User sender, CancellationToken cancellationToken)
        {
            var room = await ResolveExistingRoomAsync(request, cancellationToken);

            if (room == null && request.RecipientId != null)
            {
                var secondUser = (await _userRepository.GetByIdAsync(request.RecipientId.Value, cancellationToken)).GetResult();

                room = new Room(request.GroupId, sender, secondUser);

                await _roomsRepository.CreateAsync(room, cancellationToken);
            }

            return room;
        }

        public async Task<Room?> ResolveExistingRoomAsync(Command request, CancellationToken cancellationToken)
        {
            RepositoryEntityResult<Room> roomResult;

            if (request.RoomId != null)
            {
                roomResult = await _roomsRepository.GetRoomForUserAsync(request.GroupId, request.SenderId, request.RoomId.Value, cancellationToken);
            }
            else if (request.RecipientId != null)
            {
                roomResult = await _roomsRepository.GetPrivateRoomAsync(request.GroupId, request.SenderId, request.RecipientId.Value, cancellationToken);
            }
            else
            {
                throw new BusinessException($"At least room id or recipient id must be provided to send a message.");
            }

            if (roomResult.Status == RepositoryActionStatus.NotFound)
            {
                return null;
            }

            var room = roomResult.GetResult();

            return room;
        }
    }
}