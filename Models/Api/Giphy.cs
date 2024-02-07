namespace Api.Models.Api;

public class OriginalImage
{
    public string webp { get; set; }
}

public class ImagesObj
{
    public OriginalImage original { get; set; }
}

public class DataItem
{
    public ImagesObj images { get; set; }
}

public class GiphySearchResponse
{
    public List<DataItem> data { get; set; }
}
