using SocialNetwork.Core.Domain.Common;
using System.Collections.Generic;

namespace SocialNetwork.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public List<UserFriend> UserFriends { get; set; }
        public List<Publication> Publications { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
