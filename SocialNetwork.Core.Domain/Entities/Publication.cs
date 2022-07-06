using SocialNetwork.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<Comment> Comments { get; set; }
    }
}
