namespace SmartChat.Web.Views.viewModle
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
 
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }     
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();

    }
}
