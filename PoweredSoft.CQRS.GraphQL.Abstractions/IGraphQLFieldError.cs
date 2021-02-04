using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.Abstractions
{
    public interface IGraphQLFieldError
    {
        string Field { get; set; }
        List<string> Errors { get; set; }
    }
}
