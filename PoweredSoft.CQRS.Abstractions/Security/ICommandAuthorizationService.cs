using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.Abstractions.Security
{
    public interface ICommandAuthorizationService
    {
        Task<AuthorizationResult> IsAllowedAsync(Type commandType, CancellationToken cancellationToken = default);
    }
}
