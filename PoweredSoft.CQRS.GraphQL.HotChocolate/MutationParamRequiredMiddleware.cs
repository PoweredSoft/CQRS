using HotChocolate;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class MutationParamRequiredMiddleware
    {
        private readonly FieldDelegate _next;

        public MutationParamRequiredMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var queryArgument = context.ArgumentValue<object>("params");
            if (queryArgument == null)
            {
                context.Result = ErrorBuilder.New()
                    .SetMessage("mutation argument is required")
                    .SetCode("400")
                    .SetPath(context.Path)
                    .AddLocation(context.Selection.SyntaxNode)
                    .Build();

                return;
            }

            await _next.Invoke(context);
        }
    }
}
