﻿using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    private readonly IList<UserRoom> _roomMembers = new List<UserRoom>();
    private readonly IList<Message> _messages = new List<Message>();

    protected Room() { }

    public Room(string? name, int groupId, IEnumerable<User> users, bool isPrivate = false)
    {
        users.ThrowIfNull(nameof(users));
        Name = name;
        AddMembers(users);
        IsPrivate = isPrivate;
        GroupId = groupId;
    }

    public Room(int groupId, User firstMember, User secondMember) : this(null, groupId, new List<User> { firstMember, secondMember }, isPrivate: true) 
    {
        firstMember.ThrowIfNull(nameof(firstMember));
        secondMember.ThrowIfNull(nameof(secondMember));
    }

    public string? Name { get; private set; }

    public void AddMembers(IEnumerable<User> users)
    {
        var usersList = users.ToList();

        if (IsPrivate && usersList.Count > 2)
        {
            throw new BusinessException("Private group cannot have more than 2 members.");
        }

        foreach (var user in usersList)
        {
            AddMember(user);
        }
    }

    public void AddMember(User user)
    {
        user.ThrowIfNull(nameof(user));

        var userRoom = new UserRoom(user, this);

        _roomMembers.Add(userRoom);
    }

    public IReadOnlyCollection<UserRoom> RoomMembers => _roomMembers.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public bool IsPrivate { get; private set; } = false;

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}