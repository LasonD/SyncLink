using Newtonsoft.Json;

namespace SyncLink.Application.Services;

public interface IWordCheckerService
{
    Task<bool> IsExistingWordAsync(string word, CancellationToken cancellationToken);
}

public class WordCheckerService : IWordCheckerService
{
    private static readonly HttpClient HttpClient = new HttpClient();

    public async Task<bool> IsExistingWordAsync(string word, CancellationToken cancellationToken)
    {
        var response = await HttpClient.GetAsync($"https://api.datamuse.com/words?sp={word}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadAsStringAsync(cancellationToken);
        var words = JsonConvert.DeserializeObject<List<WordResponse>>(result);

        return words?.Any(w => w.word == word) ?? false;
    }
}

public class WordResponse
{
    public string word { get; set; } = null!;
}