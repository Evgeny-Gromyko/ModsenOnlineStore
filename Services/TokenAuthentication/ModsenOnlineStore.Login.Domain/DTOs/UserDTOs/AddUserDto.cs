using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Domain.DTOs.UserDTOs
{
    public class AddUserDTO
    {
        public string Name { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
        
        public Role Role { get; set; } = Role.User;
    }
}
