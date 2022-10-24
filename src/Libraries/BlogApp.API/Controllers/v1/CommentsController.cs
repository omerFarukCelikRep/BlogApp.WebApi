using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

public class CommentsController : BaseController
{
    private readonly ICommentService _commentService;
    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Add([FromBody] CommentCreateDto commentCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.Identity.IsAuthenticated)
        {
            commentCreateDto.UserId = UserId;
        }

        commentCreateDto.UserIpAdress = GetIpAddress();
        var result = await _commentService.AddAsync(commentCreateDto);

        return GetDataResult(result);
    }
}
