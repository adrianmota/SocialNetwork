using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comments;
using System.Threading.Tasks;

namespace WebApp.SocialNetwork.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(SaveCommentViewModel saveViewModel)
        {
            await _commentService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
