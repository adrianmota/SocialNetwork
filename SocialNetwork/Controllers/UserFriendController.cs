using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace WebApp.SocialNetwork.Controllers
{
    public class UserFriendController : Controller
    {
        private readonly IUserFriendService _userFriendService; 

        public UserFriendController(IUserFriendService userFriendService)
        {
            _userFriendService = userFriendService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userFriendService.Delete(id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }
    }
}
