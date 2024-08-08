using BlogApp.Business.Interfaces;
using BlogApp.Entities.Dtos.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers.v1;

public class CommentsController(ICommentService commentService)
    : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Add([FromBody] CommentCreateDto commentCreateDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.Identity!.IsAuthenticated)
        {
            commentCreateDto.UserId = UserId;
        }

        commentCreateDto.UserIpAdress = GetIpAddress();
        var result = await commentService.AddAsync(commentCreateDto, cancellationToken);

        return GetDataResult(result);
    }

    [HttpGet("{articleId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllByArticleId([FromRoute] Guid articleId, CancellationToken cancellationToken = default)
    {
        var result = await commentService.GetAllByArticleIdAsync(articleId, cancellationToken);

        return GetDataResult(result);
    }
}