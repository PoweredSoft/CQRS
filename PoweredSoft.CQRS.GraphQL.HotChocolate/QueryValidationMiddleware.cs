using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using Newtonsoft.Json;
using PoweredSoft.CQRS.GraphQL.Abstractions;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class QueryValidationMiddleware
    {
        private readonly FieldDelegate _next;

        public QueryValidationMiddleware(FieldDelegate next)
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