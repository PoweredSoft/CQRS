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
    public class QueryControllerAsyncAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IQueryAuthorizationService _authorizationService;

        public QueryControllerAsyncAuthorizationFilter(IServiceProvider serviceProvider)
        {
            _authorizationService = serviceProvider.GetService<IQueryAuthorizationService>();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_authorizationService == null)
                return;

            var action = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            if (action == null)
                throw new Exception("Only Supports controller action descriptor");

            var attribute = action.MethodInfo.GetCustomAttribute<QueryControllerAuthorizationAttribute>();
            Type queryType;
            if (attribute?.QueryType != null)
                queryType = attribute.QueryType;
            else
                queryType = action.ControllerTypeInfo.GenericTypeArguments.First();

            var ar = await _authorizationService.IsAllowedAsync(queryType);
            if (ar == AuthorizationResult.Forbidden)
                context.Result = new StatusCodeResult(403);
            else if (ar == AuthorizationResult.Unauthorized)
                context.Result = new StatusCodeResult(401);
        }
    }
}
