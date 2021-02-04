using PoweredSoft.CQRS.GraphQL.Abstractions;
using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.FluentValidation
{
    public class GraphQLFieldError : IGraphQLFieldError
    {
        public string Field { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
