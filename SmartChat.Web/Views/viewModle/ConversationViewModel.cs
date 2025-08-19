namespace SmartChat.Web.Views.viewModle
{
    public class UserDashboardViewModel
    {
      
            public Guid ConversationId { get; set; }
            public string ConversationTitle { get; set; } 
            public DateTime CreatedAt { get; set; }
            public DateTime? LastMessageAt { get; set; }
            public string LastMessageText { get; set; }
           public string LastMessageSenderName { get; set; }

    }
}
