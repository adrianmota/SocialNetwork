using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debes colocar la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}