using System;
using SocialNetwork.Core.Application.ViewModels.Publications;
using SocialNetwork.Core.Application.ViewModels.Users;

namespace SocialNetwork.Core.Application.ViewModels.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }

        public PublicationViewModel Publication { get; set; }
        public UserViewModel User { get; set; }
    }
}