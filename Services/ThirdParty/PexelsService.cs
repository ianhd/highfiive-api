using Api.Models;
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

    public async Task<PexelsSearchResultsPage> Search(string q, int page, int per_page)
        => await _client.GetFromJsonAsync<PexelsSearchResultsPage>($"search?query={q}&per_page={per_page}&page={page}");

    public async Task<List<SearchResultsPageV2>> GetOccasionCollections(int occasionId)
    {
        var rtn = new List<SearchResultsPageV2>();

        // for now just assume Birthday
        var searchQueries = string.Empty;
        
        switch(occasionId)
        {
            case 1: 
                searchQueries = "balloons,cupcakes";
                break;
            case 2:
                searchQueries = "flowers";
                break;
        }
        foreach(var query in searchQueries.Split(',').ToList())
        {
            var data = await Search(query, 1, 4);
            var results = new SearchResultsPageV2(data, query);
            rtn.Add(results);
        }

        return rtn;
    }
}
