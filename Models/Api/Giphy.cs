namespace Api.Models.Api;

public class OriginalImage
{
    public string webp { get; set; }
    public string height { get; set; }
    public string width { get; set; }
}

public class ImagesObj
{
    public OriginalImage original { get; set; }
}

public class DataItem
{
    public string id { get; set; }
    public ImagesObj images { get; set; }
}

public class GiphySearchResponse
{
    public List<DataItem> data { get; set; }
}
