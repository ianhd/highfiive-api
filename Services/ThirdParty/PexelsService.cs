using PexelsDotNetSDK;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;

namespace Api.Services.ThirdParty;

public class PexelsService
{
    private readonly PexelsClient _pexelsClient;

    public PexelsService(IConfiguration config)
    {
        var apiKey = config.GetValue<string>("Apis:Pexels:Key");
        _pexelsClient = new PexelsClient(apiKey);
    }

    public async Task<CollectionMediaPage> GetCollection(string id)
    {
        return await _pexelsClient.GetCollectionAsync(id: id, type: "photos");
    }
}
