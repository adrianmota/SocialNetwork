using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public string Content { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }

        public Publication Publication { get; set; }
        public User User { get; set; }
    }
}
