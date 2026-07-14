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
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; HighFiiveApi/1.0)");
    }

    public async Task<GiphySearchResponse> GetTrending()
    {
        var limit = 3;
        var offset = 0; // todo: calc this from page
        var url = $"trending?api_key={_apiKey}&limit={limit}&offset={offset}&rating=g&lang=en&bundle=messaging_non_clips";

        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            var responseHeaders = string.Join(" | ", response.Headers.Select(h => $"{h.Key}: {string.Join(",", h.Value)}"));
            var contentHeaders = string.Join(" | ", response.Content.Headers.Select(h => $"{h.Key}: {string.Join(",", h.Value)}"));
            throw new HttpRequestException($"Giphy trending request failed for URL [{_client.BaseAddress}{url}] with status {(int)response.StatusCode} ({response.StatusCode}). Body: [{body}]. ResponseHeaders: [{responseHeaders}]. ContentHeaders: [{contentHeaders}]");
        }

        return await response.Content.ReadFromJsonAsync<GiphySearchResponse>();
    }

    public async Task<GiphySearchResponse> Search(string q, int page, int limit)
    {
        var offset = (page-1) * limit;
        var url = $"search?api_key={_apiKey}&q={q}&limit={limit}&offset={offset}&rating=g&lang=en&bundle=messaging_non_clips";

        return await _client.GetFromJsonAsync<GiphySearchResponse>(url);
    }
}
