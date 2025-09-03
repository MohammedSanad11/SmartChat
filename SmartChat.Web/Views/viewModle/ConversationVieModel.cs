namespace SmartChat.Web.Views.viewModle
{
    public class ConversationVieModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int MessageCount { get; set; }
        public string LastMessageText { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public string LastMessageSenderName { get; set; }
    }
}
