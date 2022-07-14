using System.Collections.Generic;
using SocialNetwork.Core.Application.ViewModels.Publications;
using SocialNetwork.Core.Application.ViewModels.UserFriends;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Username { get; set; }

        public List<UserFriendViewModel> UserFriends { get; set; }
        public List<PublicationViewModel> Publications { get; set; }
    }
}