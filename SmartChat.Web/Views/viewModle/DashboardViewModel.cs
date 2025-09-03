namespace SmartChat.Web.Views.viewModle
{
    public class DashboardViewModel
    {
        public CurrentUserViewModel CurrentUser { get; set; }   
        public List<UserDashboardViewModel> MyConversations { get; set; } 
        public List<UserDashboardViewModel> AllConversations { get; set; }  
        public List<UserDashboardViewModel> NewChatUsers { get; set; } 
        public ChatViewModel ChatUsers { get; set; }
        public List<UserViewModel> AllUsers { get; set; }
    }
}
