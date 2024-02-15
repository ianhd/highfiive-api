using Api.Services.ThirdParty;
using Api.Services;
using System.Text.Json.Serialization;

namespace Api.Models;

public class Post
{
    [JsonIgnore]
    public int post_id { get; set; }
    public string board_eid { get; set; }
    [JsonIgnore]
    public int board_id { get; set; }
    public string body { get; set; }
    public string giphy_image { get; set; }
    public int? giphy_original_height { get; set; }
    public string body_friendly
    {
        get
        {
            if (string.IsNullOrEmpty(body)) return null;
            return body.Replace("\n", "<br/>");
        }
    }
    public string from_name { get; set; }
    public int sort_order { get; set; }
    public DateTime date_created { get; set; }
    public string date_created_friendly => date_created.ToString("MMM d, yyyy");
    public string post_eid
    {
        get
        {
            var hashIdsService = _ServiceLocator.GetService<HashIdsService>();
            return hashIdsService.Encode(post_id);
        }
    }

}
