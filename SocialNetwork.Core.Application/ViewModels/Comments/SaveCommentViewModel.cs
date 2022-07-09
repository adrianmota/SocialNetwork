namespace SocialNetwork.Core.Application.ViewModels.Comments
{
    public class SaveCommentViewModel
    {
        public string Content { get; set; }
        public string Source { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }
    }
}
