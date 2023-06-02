namespace SyncLink.Application.Domain.Feed;

public class WordsQuiz : FeedItem
{
    public override FeedItemType Type => FeedItemType.WordsQuiz;

    public string Topic { get; set; } = null!;
    public string Question { get; set; } = null!;
}

public class QuizOption
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}