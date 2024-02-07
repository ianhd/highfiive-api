namespace Api.Models.Api;

public class PexelsCollectionPage
{
    public List<PexelsPhoto> media { get; set; }
}
public class PexelsPhoto
{
    public int id { get; set; }
    public Src src { get; set; }
}

public class Src
{
    public string original { get; set; }
    public string large2x { get; set; }
    public string large { get; set; }
    public string small { get; set; }
    public string medium { get; set; }
    public string portrait { get; set; }
    public string landscape { get; set; }
    public string tiny { get; set; }
    public Src() { }
}


public class PexelsSearchResultsPage
{
    public List<PexelsPhoto> photos { get; set; }
}
