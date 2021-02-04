using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.Abstractions
{

    public interface IGraphQLValidationService
    {
        Task<IGraphQLValidationResult> ValidateObjectAsync(object subject, CancellationToken cancellationToken = default);
        Task<IGraphQLValidationResult> ValidateAsync<T>(T subject, CancellationToken cancellationToken = default);
    }
}
