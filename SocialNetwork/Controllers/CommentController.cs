using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comments;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace WebApp.SocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ValidateUserSession _validateUserSession;

        public CommentController(ICommentService commentService, ValidateUserSession validateUserSession)
        {
            _commentService = commentService;
            _validateUserSession = validateUserSession;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCommentViewModel saveViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _commentService.Add(saveViewModel);

            if (saveViewModel.Source == "fromFriend")
            {
                return RedirectToRoute(new { controller = "Friend", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}