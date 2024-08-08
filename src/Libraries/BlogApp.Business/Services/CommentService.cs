using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Comments;
using CommentMessages = BlogApp.Business.Constants.ServiceMessages.Comment;

namespace BlogApp.Business.Concrete;
public class CommentService(ICommentRepository commentRepository,
                            IUserRepository userRepository)
    : ICommentService
{
    public async Task<IResult<List<ArticleCommentListDto>?>> GetAllByArticleIdAsync(Guid articleId, CancellationToken cancellationToken = default)
    {
        var comments = await commentRepository.GetAllAsync(x => x.ArticleId == articleId, false, cancellationToken);

        return Result<List<ArticleCommentListDto>?>.Success(ObjectMapper.Mapper.Map<List<ArticleCommentListDto>>(comments), CommentMessages.Listed);
    }

    public async Task<IResult<CommentCreatedDto?>> AddAsync(CommentCreateDto commentCreateDto, CancellationToken cancellationToken = default)
    {
        var comment = ObjectMapper.Mapper.Map<Comment>(commentCreateDto);
        if (commentCreateDto.UserId.HasValue)
        {
            var user = await userRepository.GetByIdAsync(commentCreateDto.UserId.Value, false, cancellationToken);
            comment.UserName = $"{user?.FirstName} {user?.LastName}";
        }

        await commentRepository.AddAsync(comment, cancellationToken);
        await commentRepository.SaveChangesAsync(cancellationToken);

        return Result<CommentCreatedDto?>.Success(ObjectMapper.Mapper.Map<CommentCreatedDto>(comment));
    }
}