using BlogApp.Business.Constants;
using BlogApp.Business.Interfaces;
using BlogApp.Business.Mappings.Mapper;
using BlogApp.Core.Utilities.Results.Concrete;
using BlogApp.Core.Utilities.Results.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;
using BlogApp.Entities.DbSets;
using BlogApp.Entities.Dtos.Comments;

namespace BlogApp.Business.Concrete;
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public async Task<IDataResult<List<ArticleCommentListDto>>> GetAllByArticleIdAsync(Guid articleId)
    {
        var comments = await _commentRepository.GetAllAsync(x => x.ArticleId == articleId, false);

        return new SuccessDataResult<List<ArticleCommentListDto>>(ObjectMapper.Mapper.Map<List<ArticleCommentListDto>>(comments), ServiceMessages.CommentsListed);
    }

    public async Task<IDataResult<CommentCreatedDto>> AddAsync(CommentCreateDto commentCreateDto)
    {
        var comment = ObjectMapper.Mapper.Map<Comment>(commentCreateDto);
        if (commentCreateDto.UserId.HasValue)
        {
            var user = await _userRepository.GetByIdAsync(commentCreateDto.UserId.Value, false);
            comment.UserName = $"{user.FirstName} {user.LastName}";
        }

        await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveChangesAsync();

        return new SuccessDataResult<CommentCreatedDto>(ObjectMapper.Mapper.Map<CommentCreatedDto>(comment));
    }
}
