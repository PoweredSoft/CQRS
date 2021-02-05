using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Security;
using System;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class QueryAuthorizationMiddleware
    {
        private readonly Type queryType;
        private readonly FieldDelegate _next;

        public QueryAuthorizationMiddleware(Type queryType, FieldDelegate next)
        {
            this.queryType = queryType;
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var queryAuthorizationService = context.Service<IServiceProvider>().GetService<IQueryAuthorizationService>();
            if (queryAuthorizationService != null)
            {
                var authorizationResult = await queryAuthorizationService.IsAllowedAsync(queryType);
                if (authorizationResult != AuthorizationResult.Allowed)
                {
                    var eb = ErrorBuilder.New()
                               .SetMessage(authorizationResult == AuthorizationResult.Unauthorized ? "Unauthorized" : "Forbidden")
                               .SetCode("AuthorizationResult")
                               .SetExtension("StatusCode", authorizationResult == AuthorizationResult.Unauthorized ? "401" : "403")
                               .SetPath(context.Path)
                               .AddLocation(context.Selection.SyntaxNode);

                    context.Result = eb.Build();

                    return;
                }
            }


            await _next.Invoke(context);
        }
    }
}