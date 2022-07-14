using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class AddFriendViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
    }
}