using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery
{
    public abstract class DynamicQueryHandlerBase<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        private readonly IQueryHandlerAsync queryHandlerAsync;
        private readonly IEnumerable<IQueryableProvider<TSource>> queryableProviders;
        private readonly IEnumerable<IAlterQueryableService<TSource, TDestination>> alterQueryableServices;
        private readonly IEnumerable<IDynamicQueryInterceptorProvider<TSource, TDestination>> dynamicQueryInterceptorProviders;
        private readonly IServiceProvider serviceProvider;

        public DynamicQueryHandlerBase(IQueryHandlerAsync queryHandlerAsync, 
            IEnumerable<IQueryableProvider<TSource>> queryableProviders,
            IEnumerable<IAlterQueryableService<TSource, TDestination>> alterQueryableServices,
            IEnumerable<IDynamicQueryInterceptorProvider<TSource, TDestination>> dynamicQueryInterceptorProviders,
            IServiceProvider serviceProvider)
        {
            this.queryHandlerAsync = queryHandlerAsync;
            this.queryableProviders = queryableProviders;
            this.alterQueryableServices = alterQueryableServices;
            this.dynamicQueryInterceptorProviders = dynamicQueryInterceptorProviders;
            this.serviceProvider = serviceProvider;
        }

        protected virtual Task<IQueryable<TSource>> GetQueryableAsync(IDynamicQuery query, CancellationToken cancellationToken = default)
        {
            if (this.queryableProviders.Any())
                return queryableProviders.ElementAt(0).GetQueryableAsync(query, cancellationToken);

            throw new Exception($"You must provide a QueryableProvider<TSource> for {typeof(TSource).Name}");
        }

        public virtual IQueryExecutionOptions GetQueryExecutionOptions(IQueryable<TSource> source, IDynamicQuery query)
        {
            return new QueryExecutionOptions();
        }

        public virtual IEnumerable<IQueryInterceptor> GetInterceptors()
        {
            var types = dynamicQueryInterceptorProviders.SelectMany(t => t.GetInterceptorsTypes()).Distinct();
            foreach (var type in types)
                yield return serviceProvider.GetService(type) as IQueryInterceptor;
        }

        protected async Task<IQueryExecutionResult<TDestination>> ProcessQueryAsync(IDynamicQuery query, CancellationToken cancellationToken = default)
        {
            var source = await GetQueryableAsync(query, cancellationToken);
            source = await AlterSourceAsync(source, query, cancellationToken);
            var options = GetQueryExecutionOptions(source, query);
            var interceptors = this.GetInterceptors();

            foreach (var interceptor in interceptors)
                queryHandlerAsync.AddInterceptor(interceptor);

            var criteria = CreateCriteriaFromQuery(query);
            var result = await queryHandlerAsync.ExecuteAsync<TSource, TDestination>(source, criteria, options, cancellationToken);
            return result;
        }

        protected virtual async Task<IQueryable<TSource>> AlterSourceAsync(IQueryable<TSource> source, IDynamicQuery query, CancellationToken cancellationToken)
        {
            foreach (var t in alterQueryableServices)
                source = await t.AlterQueryableAsync(source, query, cancellationToken);

            return source;
        }

        protected virtual IQueryCriteria CreateCriteriaFromQuery(IDynamicQuery query)
        {
            var criteria = new QueryCriteria
            {
                Page = query?.GetPage(),
                PageSize = query?.GetPageSize(),
                Filters = query?.GetFilters() ?? new List<IFilter>(),
                Sorts = query?.GetSorts() ?? new List<ISort>(),
                Groups = query.GetGroups() ?? new List<IGroup>(),
                Aggregates = query.GetAggregates() ?? new List<IAggregate>()
            };
            return criteria;
        }
    }
}
