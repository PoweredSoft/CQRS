using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.Abstractions
{
    public interface IQueryHandler<TQuery, TQueryResult>
        where TQuery : class
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
