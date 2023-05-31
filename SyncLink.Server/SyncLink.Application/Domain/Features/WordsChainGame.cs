using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features;

public class WordsChainGame : EntityBase
{
    public string Topic { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public List<WordsChainEntry> Entries { get; set; } = null!;

    public List<UserToWordsChainGame> Participants { get; set; } = null!;
}

public class WordsChainEntry : EntityBase
{
    public string Word { get; set; } = null!;

    public int GameId { get; set; }
    public WordsChainGame Game { get; set; } = null!;

    public int SenderId { get; set; }

    public UserToWordsChainGame Participant { get; set; } = null!;
}

public class UserToWordsChainGame
{
    public int Score { get; set; }

    public bool IsCreator { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int GameId { get; set; }
    public WordsChainGame Game { get; set; } = null!;
}