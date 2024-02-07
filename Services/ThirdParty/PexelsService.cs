using Api.Models.Api;
namespace Api.Services.ThirdParty;

public class PexelsService
{
    private readonly HttpClient _client;

    public PexelsService(IConfiguration config)
    {
        var apiKey = config.GetValue<string>("Apis:Pexels:Key");
        var baseAddress = config.GetValue<string>("Apis:Pexels:BaseAddress");

        _client = new();
        _client.BaseAddress = new Uri(baseAddress);
        _client.DefaultRequestHeaders.Add("Authorization", apiKey);
    }

    public async Task<PexelsCollectionPage> GetCollection(string id)
        => await _client.GetFromJsonAsync<PexelsCollectionPage>($"collections/{id}");

    public async Task<PexelsPhoto> GetPhoto(int id)
        => await _client.GetFromJsonAsync<PexelsPhoto>($"photos/{id}");

    public async Task<PexelsSearchResultsPage> Search(string q, int page = 1)
        => await _client.GetFromJsonAsync<PexelsSearchResultsPage>($"search?query={q}&per_page=16&page={page}");
}
