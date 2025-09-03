namespace SmartChat.Web.Views.viewModle
{
    public class messageViewModel
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRole { get; set; }
        public bool IsAgent { get; set; }
    }
}

