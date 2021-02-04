using PoweredSoft.CQRS.GraphQL.Abstractions;
using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.FluentValidation
{
    public class GraphQLValidResult : IGraphQLValidationResult
    {
        public bool IsValid => true;
        public List<IGraphQLFieldError> Errors { get; } = new List<IGraphQLFieldError>();
    }
}
