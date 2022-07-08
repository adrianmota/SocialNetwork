using SocialNetwork.Core.Application.ViewModels.Comments;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.Publications
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public UserViewModel User { get; set; }
        public FriendViewModel Friend { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
