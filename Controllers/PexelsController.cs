using Api.Models;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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

    [HttpGet("pexels/collection"), AllowAnonymous]
    public async Task<IActionResult> GetCollection(int occasionId)
    {
        var data = await _pexelsService.GetCollection("zvfgiyn");
        return Ok(new SearchResultsPage(data));
    }

    [HttpGet("pexels/search"), AllowAnonymous]
    public async Task<IActionResult> Search(string q, int page = 1)
    {
        var data = await _pexelsService.Search(q, page);
        return Ok(new SearchResultsPage(data));
    }
}
