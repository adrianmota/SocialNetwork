using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Publications
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el contenido")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}
