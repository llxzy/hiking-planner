using DataAccessLayer.Enums;

namespace Application
{
    public static class Utils
    {
        public static string UserRoleToString(UserRole role)
        {
            return role switch
            {
                UserRole.Administrator => "Administrator",
                UserRole.Moderator => "Moderator",
                _ => "User"
            };
        }
    }
}