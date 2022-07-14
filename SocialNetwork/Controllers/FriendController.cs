using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friends;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace WebApp.SocialNetwork.Controllers
{
    public class FriendController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IUserFriendService _userFriendService;
        private readonly IFriendService _friendService;
        private readonly ValidateUserSession _validateUserSession;

        public FriendController(IFriendService friendService, IPublicationService publicationService, IUserFriendService userFriendService, ValidateUserSession validateUserSession)
        {
            _publicationService = publicationService;
            _userFriendService = userFriendService;
            _friendService = friendService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Publications = await _publicationService.GetAllViewModelFromFriends();
            return View(await _userFriendService.GetAllFriendViewModel());
        }

        public async Task<IActionResult> AddFriend(AddFriendViewModel addViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationService.GetAllViewModelFromFriends();
                return View("Index", await _userFriendService.GetAllFriendViewModel());
            }

            FriendViewModel friendViewModel = await _userFriendService.AddFriend(addViewModel);

            if (friendViewModel == null)
            {
                ModelState.AddModelError("friendValidation", "No existe un usuario con ese nombre");
                ViewBag.Publications = await _publicationService.GetAllViewModelFromFriends();
                return View("Index", await _userFriendService.GetAllFriendViewModel());
            }

            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }
    }
}