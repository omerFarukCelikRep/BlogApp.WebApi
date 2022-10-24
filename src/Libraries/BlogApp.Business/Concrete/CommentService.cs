using BlogApp.Business.Interfaces;
using BlogApp.DataAccess.Interfaces.Repositories;

namespace BlogApp.Business.Concrete;
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
}
