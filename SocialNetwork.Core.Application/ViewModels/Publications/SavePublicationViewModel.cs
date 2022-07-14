using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Core.Application.ViewModels.Publications
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}