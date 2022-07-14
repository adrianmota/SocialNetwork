using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Comments;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<SaveCommentViewModel, CommentViewModel, Comment> { }
}