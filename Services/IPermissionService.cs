namespace RedisExample.Services
{
    public interface IPermissionService
    {
        bool CheckUserPermission(int userID, long? actionID, int controllerID);
        string GetActionName(long? actionID,int controllerID);
        string GetControllerName(int controllerID);
    }
}
