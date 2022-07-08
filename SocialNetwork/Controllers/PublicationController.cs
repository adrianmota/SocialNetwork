using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Publications;
using System.IO;
using System.Threading.Tasks;
using WebApp.SocialNetwork.Middlewares;

namespace WebApp.SocialNetwork.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly ImageHelper _imageHelper;

        public PublicationController(IPublicationService publicationService, ValidateUserSession validateUserSession, ImageHelper imageHelper)
        {
            _publicationService = publicationService;
            _validateUserSession = validateUserSession;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePublicationViewModel saveViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (saveViewModel.File == null)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("publicationValidation", "No puedes crear una publicación sin contenido");
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }

                SavePublicationViewModel newSaveViewModel = await _publicationService.Add(saveViewModel);
            }
            else
            {
                SavePublicationViewModel newSaveViewModel = await _publicationService.Add(saveViewModel);

                int id = newSaveViewModel.Id;
                string baseImagePath = $"\\Images\\Publications\\{id}\\";
                string imageUrl = _imageHelper.UploadImage(saveViewModel.File, baseImagePath);
                newSaveViewModel.ImageUrl = imageUrl;
                await _publicationService.Update(newSaveViewModel, id);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePublicationViewModel saveViewModel = await _publicationService.GetByIdSaveViewModel(id);
            return View("SavePublication", saveViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePublicationViewModel saveViewModel)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }

            if (saveViewModel.File != null)
            {
                int id = saveViewModel.Id;
                SavePublicationViewModel oldSaveViewModel = await _publicationService.GetByIdSaveViewModel(id);
                string oldImageUrl = oldSaveViewModel.ImageUrl;
                string baseImagePath = $"\\Images\\Publications\\{id}\\";
                string imageUrl = _imageHelper.UploadImage(saveViewModel.File, baseImagePath, oldImageUrl, true);
                saveViewModel.ImageUrl = imageUrl;
                await _publicationService.Update(saveViewModel, id);
            }

            await _publicationService.Update(saveViewModel, saveViewModel.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SavePublicationViewModel saveViewModel = await _publicationService.GetByIdSaveViewModel(id);
            
            if (saveViewModel.ImageUrl != null)
            {
                string directoryOfImages = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\Publications\\{saveViewModel.Id}\\");

                if (Directory.Exists(directoryOfImages))
                {
                    DirectoryInfo directoryInfo = new(directoryOfImages);

                    foreach(DirectoryInfo directory in directoryInfo.GetDirectories())
                    {
                        directory.Delete();
                    }

                    foreach(FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    Directory.Delete(directoryOfImages);
                }
            }
            
            await _publicationService.Delete(id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
