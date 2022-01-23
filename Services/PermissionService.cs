using Dashboard.Core.Caching;
using Microsoft.EntityFrameworkCore;
using RedisExample.Core.Caching;
using RedisExample.Entities;
using RedisExample.Entities.DbContexts;

namespace RedisExample.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRedisCacheService _redisCacheManager;
        RedisDBContext _context;
        public PermissionService(IRedisCacheService redisCacheManager, RedisDBContext context)
        {
            _redisCacheManager = redisCacheManager;
            _context = context;
        }
        public bool CheckUserPermission(int userID, long? actionID, int controllerID)
        {
            //Check Redis
            var cachkeyGetUserPermissionByUserId = string.Format(CacheKeys.GetUserPermissionByUserId, userID);

            var getUserPermissionByUserId = _redisCacheManager.Get<List<DbSecurityUserAction>>(cachkeyGetUserPermissionByUserId);
            if (getUserPermissionByUserId == null)
            {
                var response = _context.DbSecurityUserActions.AsNoTracking().Where(user => user.IdUser == userID).ToList();
                if (response != null)
                {
                    _redisCacheManager.Set(cachkeyGetUserPermissionByUserId, response);
                    var userActionPermision = response.Where(sa => sa.IdSecurityController == controllerID).FirstOrDefault();
                    if (userActionPermision != null)
                    {
                        long ActionNumberTotal = (long)userActionPermision.ActionNumberTotal;
                        return (actionID == (ActionNumberTotal & actionID));
                    }
                    else return false;
                }
            }
            else
            {
                var userActionPermision = getUserPermissionByUserId.Where(sa => sa.IdSecurityController == controllerID).FirstOrDefault();
                if (userActionPermision != null)
                {
                    long ActionNumberTotal = (long)userActionPermision.ActionNumberTotal;
                    return (actionID == (ActionNumberTotal & actionID));
                }
                else return false;
            }
            return false;
        }

        public string GetActionName(long? actionID,int controllerID)
        {
            //Check Redis
            var cacheKeyGetAllActions = string.Format(CacheKeys.GetAllActions);

            var getAllActionsResult = _redisCacheManager.Get<List<DbSecurityAction>>(cacheKeyGetAllActions);
            if (getAllActionsResult == null)
            {
                var response = _context.DbSecurityActions.AsNoTracking().ToList();
                if (response != null)
                {
                    _redisCacheManager.Set(cacheKeyGetAllActions, response);

                    string actionName = response.Where(action => action.ActionNumber == actionID && action.IdSecurityController== controllerID)
                        .FirstOrDefault().ActionName;
                    return actionName;
                }
            }
            else
            {
                string actionName = getAllActionsResult.Where(action => action.ActionNumber == actionID && action.IdSecurityController == controllerID)
                    .FirstOrDefault().ActionName;
                return actionName;
            }
            return string.Empty;
        }

        public string GetControllerName(int controllerID)
        {
            //Check Redis
            var cacheKeyGetAllControllers = string.Format(CacheKeys.GetAllControllers);

            var getAllControllersResult = _redisCacheManager.Get<List<DbSecurityController>>(cacheKeyGetAllControllers);
            if (getAllControllersResult == null)
            {
                var response = _context.DbSecurityControllers.AsNoTracking().ToList();
                if (response != null)
                {
                    _redisCacheManager.Set(cacheKeyGetAllControllers, response);

                    string controllerName = response.Where(controller => controller.IdSecurityController == controllerID).
                        FirstOrDefault().ControllerName;
                    return controllerName;
                }
            }
            else
            {
                string controllerName = getAllControllersResult.Where(controller => controller.IdSecurityController == controllerID)
                    .FirstOrDefault().ControllerName;
                return controllerName;
            }
            return string.Empty;
        }
    }
}
