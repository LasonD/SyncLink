namespace SyncLink.Application.Domain;

public class Language
{
    public string Name { get; set; } = null!;
    public LanguageLevel Level { get; set; }
}

public enum LanguageLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Native
}