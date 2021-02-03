using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery
{
    public class DynamicQueryHandler<TSource, TDestination>
        : DynamicQueryHandlerBase<TSource, TDestination>, 
        PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination>, IQueryExecutionResult<TDestination>>
        where TSource : class
        where TDestination : class
    {
        public DynamicQueryHandler(IQueryHandlerAsync queryHandlerAsync, IEnumerable<IQueryableProvider<TSource>> queryableProviders) : base(queryHandlerAsync, queryableProviders)
        {
        }

        public Task<IQueryExecutionResult<TDestination>> HandleAsync(IDynamicQuery<TSource, TDestination> query, CancellationToken cancellationToken = default)
        {
            return ProcessQueryAsync(query, cancellationToken);
        }
    }

    public class DynamicQueryHandler<TSource, TDestination, TParams>
        : DynamicQueryHandlerBase<TSource, TDestination>, 
        PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination, TParams>, IQueryExecutionResult<TDestination>>
        where TSource : class
        where TDestination : class
        where TParams : class
    {
        public DynamicQueryHandler(IQueryHandlerAsync queryHandlerAsync, IEnumerable<IQueryableProvider<TSource>> queryableProviders) : base(queryHandlerAsync, queryableProviders)
        {
        }

        public Task<IQueryExecutionResult<TDestination>> HandleAsync(IDynamicQuery<TSource, TDestination, TParams> query, CancellationToken cancellationToken = default)
        {
            return this.ProcessQueryAsync(query, cancellationToken);
        }
    }
}
