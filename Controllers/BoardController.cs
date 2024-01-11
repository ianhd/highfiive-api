using Api.Models;
using Api.Services;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class BoardController : ControllerBase
{
    private readonly BoardService _boardService;

    public BoardController(BoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpPost("board")]
    public async Task<IActionResult> InsertBoard(Board item)
    {
        var userId = (int)Request.HttpContext.Items["UserId"];
        item.user_id = userId;
        return Ok(await _boardService.Insert(item));
    }

    [HttpGet("boards"), AllowAnonymous]
    public async Task<IActionResult> GetUserBoards()
    {
        var userId = 1;
        return Ok(await _boardService.GetAll(1));
    }
}
