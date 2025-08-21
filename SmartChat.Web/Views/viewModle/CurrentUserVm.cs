namespace SmartChat.Web.Views.viewModle
{
    public class CurrentUserVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int ConversationCount { get; set; }
    }
}
