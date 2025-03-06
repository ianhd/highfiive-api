using Api.Models;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class PexelsController : ControllerBase
{
    private readonly PexelsService _pexelsService;

    public PexelsController(PexelsService pexelsService)
    {
        _pexelsService = pexelsService;
    }

    [HttpGet("pexels/search"), AllowAnonymous]
    public async Task<IActionResult> SearchV2(string q, int page = 1, int limit = 16)
    {
        var data = await _pexelsService.Search(q, page, limit);
        return Ok(new SearchResultsPageV2(data, q));
    }

    [HttpGet("pexels/occasion-collections"), AllowAnonymous]
    public async Task<IActionResult> GetOccasionCollections(int occasionId)
    {
        var data = await _pexelsService.GetOccasionCollections(occasionId);
        return Ok(data); // List<SearchResultsPageV2>
    }
}
