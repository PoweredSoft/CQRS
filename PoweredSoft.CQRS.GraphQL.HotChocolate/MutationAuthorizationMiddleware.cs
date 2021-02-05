using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Security;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    internal class MutationAuthorizationMiddleware
    {
        private readonly Type mutationType;
        private readonly FieldDelegate _next;

        public MutationAuthorizationMiddleware(Type mutationType,FieldDelegate next)
        {
            this.mutationType = mutationType;
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var mutationAuthorizationService = context.Service<IServiceProvider>().GetService<ICommandAuthorizationService>();
            if (mutationAuthorizationService != null)
            {
                var authorizationResult = await mutationAuthorizationService.IsAllowedAsync(mutationType);
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