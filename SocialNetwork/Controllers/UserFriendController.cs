using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace WebApp.SocialNetwork.Controllers
{
    public class UserFriendController : Controller
    {
        private readonly IUserFriendService _userFriendService;
        private readonly ValidateUserSession _validateUserSession;

        public UserFriendController(IUserFriendService userFriendService, ValidateUserSession validateUserSession)
        {
            _userFriendService = userFriendService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _userFriendService.Delete(id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }
    }
}