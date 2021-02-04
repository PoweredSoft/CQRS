using HotChocolate;
using HotChocolate.Resolvers;
using PoweredSoft.CQRS.GraphQL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class MutationValidationMiddleware
    {
        private readonly FieldDelegate _next;

        public MutationValidationMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var queryArgument = context.ArgumentValue<object>("params");
            if (queryArgument != null)
            {
                var service = context.Service<IGraphQLValidationService>();
                var result = await service.ValidateObjectAsync(queryArgument, context.RequestAborted);
                if (!result.IsValid)
                {
                    var eb = ErrorBuilder.New()
                               .SetMessage("There are some validations errors")
                               .SetCode("ValidationError")
                               .SetPath(context.Path)
                               .AddLocation(context.Selection.SyntaxNode);

                    foreach (var error in result.Errors)
                        eb.SetExtension(error.Field, error.Errors);

                    context.Result = eb.Build();

                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
