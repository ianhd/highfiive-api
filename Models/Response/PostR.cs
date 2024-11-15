namespace Api.Models.Response;

public class PostR
{
    public string img { get; set; }
    public int? img_w { get; set; }
    public int? img_h { get; set; }
    public string body { get; set; }
    public string from { get; set; }
    public string eid { get; set; }

    public PostR(Models.Post post)
    {
        img = post.giphy_image;
        img_w = post.giphy_original_width;
        img_h = post.giphy_original_height;
        body = post.body_friendly;
        from = post.from_name;
        eid = post.post_eid;
    }
}
