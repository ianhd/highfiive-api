using Api.Models.Api;

namespace Api.Models;

public class SearchResultsPage
{
    public record Result (string key, string value);
    public List<Result> Results { get; set; } = new List<Result> ();

    public string Query { get; set; }

    public SearchResultsPage(PexelsSearchResultsPage pexelsSearchResultsPage, string query)
    {
        Query = query;
        foreach (var item in pexelsSearchResultsPage.photos)
        {
            Results.Add(new Result(item.id.ToString(), item.src.tiny));
        }
    }

    public SearchResultsPage(PexelsCollectionPage pexelsCollectionPage)
    {
        foreach (var item in pexelsCollectionPage.media)
        {
            Results.Add(new Result(item.id.ToString(), item.src.large));
        }
    }

    public SearchResultsPage(GiphySearchResponse giphySearchResponse)
    {
        foreach(var item in giphySearchResponse.data)
        {
            var height = 0;
            try
            {
                height = int.Parse(item.images.original.height);
            }catch { }
            Results.Add(new Result(height.ToString(), item.images.original.webp));
        }
    }
}
