﻿using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups;

namespace SyncLink.Application.Domain.Features;

public class WordsChainGame : EntityBase
{
    public string Topic { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public List<WordsChainEntry> Entries { get; set; } = null!;

    public List<UserWordsChainGame> Participants { get; set; } = null!;
}

public class WordsChainEntry : EntityBase
{
    public string Word { get; set; } = null!;

    public int GameId { get; set; }
    public WordsChainGame Game { get; set; } = null!;

    public int UserId { get; set; }

    public UserWordsChainGame Participant { get; set; } = null!;
}

public class UserWordsChainGame
{
    public int Score { get; set; }

    public bool IsCreator { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int GameId { get; set; }
    public WordsChainGame Game { get; set; } = null!;
}