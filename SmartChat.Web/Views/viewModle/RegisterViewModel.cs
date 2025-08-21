using System.ComponentModel.DataAnnotations;

namespace SmartChat.Web.Views.viewModle
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();

    }
}
