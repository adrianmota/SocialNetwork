namespace SocialNetwork.Core.Domain.Entities
{
    public class UserFriend
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int FriendId { get; set; }
        public Friend Friend { get; set; }
    }
}