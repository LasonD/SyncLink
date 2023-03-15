using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Commands.CreateGroup;

public partial class CreateGroup
{
    public class Handler : IRequestHandler<Command, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            var user = userResult.GetResult();

            var userHasGroupWithSameName = await _userRepository.UserHasGroupWithNameAsync(user.Id, request.Name, cancellationToken);

            if (userHasGroupWithSameName)
            {
                throw new BusinessException($"User {user.UserName} already has group with name {request.Name}");
            }

            var group = new Group(request.Name, request.Description);

            user.AddGroup(group, isCreator: true, isAdmin: true);

            await _userRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<GroupDto>(group);

            return dto;
        }
    }
}