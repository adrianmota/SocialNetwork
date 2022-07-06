using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Publications;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace WebApp.SocialNetwork.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly ValidateUserSession _validateUserSession;

        public PublicationController(IPublicationService publicationService, ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
            _publicationService = publicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePublicationViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }

            await _publicationService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
