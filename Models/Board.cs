using Api.Models.Api;
using Api.Services;
using Api.Services.ThirdParty;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace Api.Models;

public class Board
{

    [JsonIgnore]
    public int board_id { get; set; }
    [JsonIgnore]
    public int user_id { get; set; }
    public int num_posts { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string title { get; set; }
    public int pexels_photo_id { get; set; }
    public int occasion_id { get; set; }
    public DateTime date_created { get; set; }
    public PexelsPhoto pexels_photo { get; set; } = new();

    // helpers
    public string date_created_friendly => date_created.ToString("MMM d, yyyy");
    
    public string board_eid
    {
        get
        {
            var hashIdsService = _ServiceLocator.GetService<HashIdsService>();
            return hashIdsService.Encode(board_id);
        }
    }

    public string url
    {
        get
        {
            var settings = _ServiceLocator.GetService<SiteSettings>();
            return $"{settings.SiteUrl}/b/{board_eid}";
        }
    }
    public string url_contribute => url + "/contribute";
    public string url_admin => url + "/admin";
}
