using Api.Services;
using Api.Services.ThirdParty;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;

namespace Api.Models;

public class Board
{
    public int board_id { get; set; }
    public int user_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string title { get; set; }
    public int pexels_photo_id { get; set; }
    public int occasion_id { get; set; }
    public DateTime date_created { get; set; }
    
    public string url
    {
        get
        {
            var settings = _ServiceLocator.GetService<SiteSettings>();
            var hashIdsService = _ServiceLocator.GetService<HashIdsService>();
            return $"{settings.SiteUrl}/b/{hashIdsService.Encode(board_id)}";
        }
    }
    public string url_contribute => url + "/contribute";
    public string url_admin => url + "/admin";
}
