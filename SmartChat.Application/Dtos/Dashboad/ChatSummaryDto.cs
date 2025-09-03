namespace SmartChat.Application.Dtos.Dashboad
{
    public class ChatSummaryDto
    {
        public int TotalUsers { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalAgents { get; set; }
        public int TotalNormalUsers { get; set; }
        public int TotalOpenChats { get; set; }
    }
}