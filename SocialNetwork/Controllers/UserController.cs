using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Users;
using System.Threading.Tasks;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Infrastructure.Shared.Services;
using SocialNetwork.Core.Application.Dtos;
using Microsoft.AspNetCore.Http;
using WebApp.SocialNetwork.Middlewares;
using SocialNetwork.Core.Application.ViewModels.Friends;

namespace WebApp.SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;
        private readonly IEmailService _emailService;
        private readonly ImageHelper _imageHelper;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ImageHelper imageHelper, IEmailService emailService, IFriendService friendService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _imageHelper = imageHelper;
            _emailService = emailService;
            _friendService = friendService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            UserViewModel userViewModel = await _userService.Login(loginViewModel);

            if (userViewModel == null)
            {
                ModelState.AddModelError("userValidation", "Los datos de acceso son incorrectos");
                return View();
            }

            if (!userViewModel.Active)
            {
                ModelState.AddModelError("userValidation", "No puedes iniciar sesión porque tu usuario no ha sido activado");
                return View();
            }

            HttpContext.Session.Set<UserViewModel>("user", userViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public IActionResult ResetPassword()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new ResetUserPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetUserPasswordViewModel resetPasswordViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            UserViewModel viewModel = await _userService.ResetPassword(resetPasswordViewModel);

            if (viewModel == null)
            {
                ModelState.AddModelError("userValidation", "El usuario especificado no existe");
                return View(resetPasswordViewModel);
            }

            await _emailService.SendAsync(new EmailRequest
            {
                To = viewModel.Email,
                Subject = "Contraseña reestablecida",
                Body = $"<h2>Hey {viewModel.FirstName}, tu contraseña ha sido reestablecida</h2>" +
                       $"<p>Esta es tu nueva contraseña: {viewModel.Password}</p>"
            });

            return View("PasswordReset", viewModel);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }

            SaveUserViewModel newSaveViewModel = await _userService.Add(saveViewModel);

            if (newSaveViewModel == null)
            {
                ModelState.AddModelError("userValidation", "El nombre de usuario no está disponible");
                return View(saveViewModel);
            }

            int id = newSaveViewModel.Id;
            string baseImagePath = $"\\Images\\Users\\{id}\\";
            string imageUrl = _imageHelper.UploadImage(saveViewModel.File, baseImagePath);
            newSaveViewModel.ProfilePhotoUrl = imageUrl;
            await _userService.Update(newSaveViewModel, id);

            SaveFriendViewModel saveFriendViewModel = await _friendService.GetByIdSaveViewModel(id);
            saveFriendViewModel.ProfilePhotoUrl = imageUrl;
            await _friendService.Update(saveFriendViewModel, id);

            //Send email after the user has been created
            await _emailService.SendAsync(new EmailRequest
            {
                To = saveViewModel.Email,
                Subject = "Activar usuario",
                Body = "<h1>Gracias por registrarte a Social Network :)</h1>" +
                       "<p>Presiona el hipervínculo debajo para activar el usuario:</p>" +
                       $"<p><a href='https://localhost:44388/User/Activate/{id}'>Activar usuario</a></p>"
            });

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Activate(int id)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Activate(id);
            return View("UserAvailable");
        }
    }
}