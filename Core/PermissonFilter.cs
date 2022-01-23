using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using RedisExample.Services;

namespace RedisExample.Core
{
    public class PermissonFilter : IActionFilter
    {
        IPermissionService _permissonService;
        public PermissonFilter(IPermissionService permissonService)
        {
            _permissonService = permissonService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int userID = int.Parse(context.RouteData.Values["userID"].ToString());
            //Action Yetkisine bakılır.
            if (HasSecurityActionAttribute(context))
            {
                try
                {
                    var arguments = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes.
                        FirstOrDefault(fd => fd.AttributeType == typeof(SecurityActionAttribute)).ConstructorArguments;

                    int idSecurityController = (int)arguments[0].Value;
                    long? actionNumber = null;
                    actionNumber = (long)arguments[1].Value;

                    if (!_permissonService.CheckUserPermission(userID, actionNumber, idSecurityController))
                    {
                        string actionName = _permissonService.GetActionName(actionNumber, idSecurityController);
                        string controllerName = _permissonService.GetControllerName(idSecurityController);
                        //Forbidden 403 Result. Yetkiniz Yoktur..
                        context.Result = new ObjectResult(context.ModelState)
                        {
                            Value = $"\"{controllerName}\" sayfa için, \"{actionName}\" işlemi için geçerli bir yetkiniz yoktur.",
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden
                        };
                    }

                }
                catch
                {

                }
            }

        }

        public bool HasSecurityActionAttribute(FilterContext context)
        {
            return ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes.
                Any(filterDescriptors => filterDescriptors.AttributeType == typeof(SecurityActionAttribute));
        }
    }
}
