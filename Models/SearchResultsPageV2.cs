using Api.Models.Api;

namespace Api.Models;

public class SearchResultsPageV2
{
    public record Result(string id, int width, int height, string url)
    {
        //public double aspectRatio => Math.Round((double)width / height, 2);
    };
    public List<Result> Results { get; set; } = new List<Result>();

    public string Query { get; set; }

    public SearchResultsPageV2(PexelsSearchResultsPage pexelsSearchResultsPage, string query)
    {
        Query = query;
        foreach (var item in pexelsSearchResultsPage.photos)
        {
            Results.Add(new Result(item.id.ToString(), item.width, item.height, item.src.tiny));
        }
    }

    public SearchResultsPageV2(PexelsCollectionPage pexelsCollectionPage)
    {
        foreach (var item in pexelsCollectionPage.media)
        {
            Results.Add(new Result(item.id.ToString(), item.width, item.height, item.src.large));
        }
    }

    public SearchResultsPageV2(GiphySearchResponse giphySearchResponse)
    {
        foreach (var item in giphySearchResponse.data)
        {
            var width = 0;
            var height = 0;
            try
            {
                width = int.Parse(item.images.original.width);
                height = int.Parse(item.images.original.height);
            }
            catch { }
            Results.Add(new Result(item.id, width, height, item.images.original.webp));
        }
    }
}
