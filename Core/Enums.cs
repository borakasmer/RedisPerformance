namespace RedisExample.Core
{
    public class Enums
    {
        public enum HomeControllers
        {
            User = 2,
            Report = 1,
            UserPermission = 7

        }       
        public enum UserPermissionActions
        {
            GetUserPermissionsById = 1,
            GetRolePermissionsById = 2,
            AddRolePermission = 4,               
        }
    }
}
