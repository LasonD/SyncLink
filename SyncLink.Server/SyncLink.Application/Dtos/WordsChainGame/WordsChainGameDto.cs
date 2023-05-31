using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Dtos.WordsChainGame;

public class WordsChainGameDto
{
    public int Id { get; set; }
    public string Topic { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public int WordsCount { get; set; }

    //public IPaginatedResult<WordsChainGameEntryDto> Entries { get; set; } = null!;
    public IPaginatedResult<WordsChainGameParticipantDto> Participants { get; set; } = null!;
}