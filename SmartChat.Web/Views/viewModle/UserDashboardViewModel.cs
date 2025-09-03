namespace SmartChat.Web.Views.viewModle
{
    public class UserDashboardViewModel
    {
        public Guid UserId { get; set; }
        public int TotalChats { get; set; }
        public int ActiveChats { get; set; }
        public int DailyMessages { get; set; }
        public double ActiveChatPercentage { get; set; }
    }
}
