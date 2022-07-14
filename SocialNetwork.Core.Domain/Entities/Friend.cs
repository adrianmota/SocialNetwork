using System.Collections.Generic;
using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Friend : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Username { get; set; }

        public List<UserFriend> UserFriends { get; set; }
        public List<Publication> Publications { get; set; }
    }
}