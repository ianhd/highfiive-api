using Api.Models;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class GiphyController : ControllerBase
{
    private readonly GiphyService _giphyService;

    public GiphyController(GiphyService giphyService)
    {
        _giphyService = giphyService;
    }

    [HttpGet("giphy/trending"), AllowAnonymous]
    public async Task<IActionResult> Trending()
    {
        var data = await _giphyService.GetTrending();
        return Ok(new SearchResultsPage(data));
    }

    [HttpGet("giphy/search"), AllowAnonymous]
    public async Task<IActionResult> Search(string q, int page = 1)
    {
        var data = await _giphyService.Search(q, page);
        return Ok(new SearchResultsPage(data));
    }
}
