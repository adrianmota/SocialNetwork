using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Users
{
    public class ResetUserPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
    }
}
