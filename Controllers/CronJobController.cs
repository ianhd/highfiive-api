using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class CronJobController : ControllerBase
{
    private readonly BoardService _boardService;

    public CronJobController(BoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet("cron/db-ping"), AllowAnonymous]
    public async Task<IActionResult> GetBoard()
        => Ok(await _boardService.DbPing());
}
