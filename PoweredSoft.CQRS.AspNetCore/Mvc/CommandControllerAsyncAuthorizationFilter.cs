using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using PoweredSoft.CQRS.Abstractions.Security;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class CommandControllerAsyncAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ICommandAuthorizationService _authorizationService;

        public CommandControllerAsyncAuthorizationFilter(IServiceProvider serviceProvider)
        {
            _authorizationService = serviceProvider.GetService<ICommandAuthorizationService>();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_authorizationService == null)
                return;

            var action = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            if (action == null)
                throw new Exception("Only Supports controller action descriptor");

            var attribute = action.MethodInfo.GetCustomAttribute<CommandControllerAuthorizationAttribute>();
            Type commandType;
            if (attribute?.CommandType != null)
                commandType = attribute.CommandType;
            else
                commandType = action.ControllerTypeInfo.GenericTypeArguments.First();

            var ar = await _authorizationService.IsAllowedAsync(commandType);
            if (ar == AuthorizationResult.Forbidden)
                context.Result = new StatusCodeResult(403);
            else if(ar == AuthorizationResult.Unauthorized)
                context.Result = new StatusCodeResult(401);
        }
    }
}
