using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.Abstractions.Security
{

    public interface IQueryAuthorizationService
    {
        Task<AuthorizationResult> IsAllowedAsync(Type queryType, CancellationToken cancellationToken = default);
    }
}
