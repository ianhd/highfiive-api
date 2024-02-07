using Api.Models.Api;

namespace Api.Models;

public class SearchResultsPage
{
    public record Result (string key, string value);
    public List<Result> Results { get; set; } = new List<Result> ();

    public SearchResultsPage(PexelsSearchResultsPage pexelsSearchResultsPage)
    {
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
            Results.Add(new Result(null, item.images.original.webp));
        }
    }
}
