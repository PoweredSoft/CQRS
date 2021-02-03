using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;
using System.Linq;
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
        public DynamicQueryHandler(IQueryHandlerAsync queryHandlerAsync, 
            IEnumerable<IQueryableProvider<TSource>> queryableProviders,
            IEnumerable<IAlterQueryableService<TSource, TDestination>> alterQueryableServices) : base(queryHandlerAsync, queryableProviders, alterQueryableServices)
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
        private readonly IEnumerable<IAlterQueryableService<TSource, TDestination, TParams>> alterQueryableServicesWithParams;

        public DynamicQueryHandler(IQueryHandlerAsync queryHandlerAsync, 
            IEnumerable<IQueryableProvider<TSource>> queryableProviders,
            IEnumerable<IAlterQueryableService<TSource, TDestination>> alterQueryableServices,
            IEnumerable<IAlterQueryableService<TSource, TDestination, TParams>> alterQueryableServicesWithParams) : base(queryHandlerAsync, queryableProviders, alterQueryableServices)
        {
            this.alterQueryableServicesWithParams = alterQueryableServicesWithParams;
        }

        protected override async Task<IQueryable<TSource>> AlterSourceAsync(IQueryable<TSource> source, IDynamicQuery query, CancellationToken cancellationToken)
        {
            source =  await base.AlterSourceAsync(source, query, cancellationToken);

            if (query is IDynamicQueryParams<TParams> withParams)
            {
                foreach (var it in alterQueryableServicesWithParams)
                    source = await it.AlterQueryableAsync(source, withParams, cancellationToken);
            }

            return source;
        }

        public Task<IQueryExecutionResult<TDestination>> HandleAsync(IDynamicQuery<TSource, TDestination, TParams> query, CancellationToken cancellationToken = default)
        {
            return this.ProcessQueryAsync(query, cancellationToken);
        }
    }
}
