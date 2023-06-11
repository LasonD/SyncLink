using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Groups.Feed;

public class WordsQuiz : FeedItem
{
    public override FeedItemType Type => FeedItemType.WordsQuiz;

    public string Topic { get; set; } = null!;
    public string Question { get; set; } = null!;

    public List<QuizOption> Options { get; set; } = null!;
}

public class QuizOption : EntityBase
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}