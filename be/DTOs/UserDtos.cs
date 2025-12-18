using be.Models;

namespace be.DTOs
{
    public class UserRegisterDto
    {
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string roleId { get; set; } = string.Empty;
    }

    public class UserLoginDto
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }

    public class RoleDto
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class UserResponseDto
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public RoleDto role { get; set; }
    }


}