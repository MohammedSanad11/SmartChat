namespace SmartChat.Web.Views.viewModle
{
    public class DashboardViewModel
    {
        public CurrentUserVm CurrentUser { get; set; }   
        public List<UserDashboardViewModel> Conversations { get; set; }

        public List<UserDashboardViewModel> MyConversations { get; set; } = new List<UserDashboardViewModel>();
        public List<UserDashboardViewModel> AllConversations { get; set; } = new List<UserDashboardViewModel>();
        public List<UserVm> AllUsers { get; set; }
    }
}
