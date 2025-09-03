namespace SmartChat.Web.Views.viewModle
{
    public class CurrentUserViewModel
    {
        public Guid Id { get; set; }
        public string Email {  get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int ConversationCount { get; set; }
    }
}
