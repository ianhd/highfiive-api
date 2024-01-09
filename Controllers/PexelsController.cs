using Api.Services.ThirdParty;
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

    [HttpGet("pexels/collection")]
    public async Task<IActionResult> GetCollection(int occasionId)
        => Ok(await _pexelsService.GetCollection("zvfgiyn"));
}
