using System.Collections.Generic;
using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Publication : AuditableBaseEntity
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public User User { get; set; }
        public Friend Friend { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
