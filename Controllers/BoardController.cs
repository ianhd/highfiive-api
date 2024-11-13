using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
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
        if(item.pexels_photo_id == 0)
        {
            throw new Exception("pexels_photo_id is 0");
        }
        var user_id = (int)Request.HttpContext.Items["UserId"];
        item.user_id = user_id;
        return Ok(await _boardService.Insert(item));
    }

    [HttpGet("board/{board_eid}"), AllowAnonymous]
    public async Task<IActionResult> GetBoard(string board_eid)
    {
        return Ok(await _boardService.Get(board_eid));
    }

    [HttpGet("board-del/{board_eid}"), AllowAnonymous]
    public async Task DeleteBoard(string board_eid)
        => await _boardService.Delete(board_eid);

    [HttpGet("boards")]
    public async Task<IActionResult> GetUserBoards()
    {
        var user_id = (int)Request.HttpContext.Items["UserId"];
        return Ok(await _boardService.GetAll(user_id));
    }
}
