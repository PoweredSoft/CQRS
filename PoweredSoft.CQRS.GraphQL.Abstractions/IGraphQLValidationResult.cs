using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.Abstractions
{
    public interface IGraphQLValidationResult
    {
        bool IsValid { get; }

        List<IGraphQLFieldError> Errors { get; }
    }
}
