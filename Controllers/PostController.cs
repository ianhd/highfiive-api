﻿using Api.Models;
using Api.Services;
using Api.Services.ThirdParty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [HttpPost("post"), AllowAnonymous]
    public async Task InsertPost(Post item)
        => await _postService.Insert(item);

    [HttpGet("post-del/{post_eid}"), AllowAnonymous]
    public async Task DeletePost(string post_eid)
        => await _postService.Delete(post_eid);

    [HttpGet("posts"), AllowAnonymous]
    public async Task<List<Post>> GetPostsForBoard(string board_eid)
        => await _postService.GetForBoard(board_eid);
}
