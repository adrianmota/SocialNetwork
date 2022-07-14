using System.Collections.Generic;
using SocialNetwork.Core.Application.ViewModels.Publications;
using SocialNetwork.Core.Application.ViewModels.UserFriends;

namespace SocialNetwork.Core.Application.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public List<PublicationViewModel> Publications { get; set; }
        public List<UserFriendViewModel> UserFriends { get; set; }
    }
}