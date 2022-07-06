using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.ViewModels.UserFriends
{
    public class UserFriendViewModel
    {
        public int UserId { get; set; }
        public UserViewModel User { get; set; }

        public int FriendId { get; set; }
        public FriendViewModel Friend { get; set; }
    }
}
