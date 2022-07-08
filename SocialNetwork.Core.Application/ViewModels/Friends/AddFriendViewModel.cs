using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class AddFriendViewModel
    {
        [Required(ErrorMessage = "Debes colocar el nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
    }
}
