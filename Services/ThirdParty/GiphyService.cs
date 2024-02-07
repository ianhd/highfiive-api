using Api.Models.Api;
namespace Api.Services.ThirdParty;

public class GiphyService
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public GiphyService(IConfiguration config)
    {
        _apiKey = config.GetValue<string>("Apis:Giphy:Key");
        
        var baseAddress = config.GetValue<string>("Apis:Giphy:BaseAddress");
        _client = new();
        _client.BaseAddress = new Uri(baseAddress);
    }

    public async Task<GiphySearchResponse> GetTrending()
    {
        var limit = 3;
        var offset = 0; // todo: calc this from page
        var url = $"trending?api_key={_apiKey}&limit={limit}&offset={offset}&rating=g&lang=en&bundle=messaging_non_clips";

        return await _client.GetFromJsonAsync<GiphySearchResponse>(url);
    }

    public async Task<GiphySearchResponse> Search(string q, int page = 1)
    {
        var limit = 12;
        var offset = (page-1) * limit;
        var url = $"search?api_key={_apiKey}&q={q}&limit={limit}&offset={offset}&rating=g&lang=en&bundle=messaging_non_clips";

        return await _client.GetFromJsonAsync<GiphySearchResponse>(url);
    }
}
