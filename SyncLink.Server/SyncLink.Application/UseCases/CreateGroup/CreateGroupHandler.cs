using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.CreateGroup;

public partial class CreateGroup
{
    public class Handler : IRequestHandler<Command, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public Handler(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<GroupDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken, 
                 include: u => u.UserGroups);

            var user = userResult.GetResult();

            if (user.UserGroups.Any(ug => ug.Group.Name == request.Name))
            {
                throw new BusinessException($"User {user.UserName} already has group with name {request.Name}");
            }

            var group = new Group(request.Name, request.Description);

            var userGroup = new UserGroup(user, group, isCreator: true, isAdmin: true);

            _groupRepository.CreateAsync();
        }
    }
}

