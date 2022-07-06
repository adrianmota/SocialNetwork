using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Comments
{
    public class SaveCommentViewModel
    {
        [Required(ErrorMessage = "Debes colocar el contenido")]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        public int PublicationId { get; set; }
        public int UserId { get; set; }
    }
}
