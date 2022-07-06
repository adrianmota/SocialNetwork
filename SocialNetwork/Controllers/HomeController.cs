using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Publications;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService _publicationService; 
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IPublicationService publicationService, ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
            _publicationService = publicationService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            List<PublicationViewModel> publicationsList = await _publicationService.GetAllViewModelFromUser();
            return View(publicationsList);
        }
    }
}
