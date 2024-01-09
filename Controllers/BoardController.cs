using Api.Models;
using Api.Services;
using Api.Services.ThirdParty;
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
    public async Task<IActionResult> SaveBoard(Board item)
        => Ok(await _boardService.Insert(item));
}
