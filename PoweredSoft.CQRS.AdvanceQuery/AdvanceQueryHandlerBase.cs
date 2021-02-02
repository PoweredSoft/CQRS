using PoweredSoft.AdvanceQuery.Abstractions;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.AdvanceQuery
{
    public abstract class AdvanceQueryHandlerBase<TQuery, TSource, TDestination>
        where TQuery : class
    {
        private readonly IQueryHandlerAsync queryHandlerAsync;

        protected AdvanceQueryHandlerBase(IQueryHandlerAsync queryHandlerAsync)
        {
            this.queryHandlerAsync = queryHandlerAsync;
        }

        protected async Task<IQueryExecutionResult<TDestination>> ProcessQuery(TQuery query, IQueryable<TSource> source, 
            CancellationToken cancellationToken = default)
        {
            var criteria = CreateCriteria(query);
            var options = GetOptions(query);
            var interceptors = GetInterceptors(query);

            foreach (var interceptor in interceptors)
                queryHandlerAsync.AddInterceptor(interceptor);

            var result = await queryHandlerAsync.ExecuteAsync<TSource, TDestination>(source, criteria, options, cancellationToken);
            return result;
        }

        protected virtual IEnumerable<IQueryInterceptor> GetInterceptors(TQuery query)
        {
            return Enumerable.Empty<IQueryInterceptor>();
        }

        protected virtual IQueryExecutionOptions GetOptions(TQuery query)
        {
            return new QueryExecutionOptions();
        }

        protected virtual IQueryCriteria CreateCriteria(TQuery query)
        {
            var ret = new QueryCriteria();

            if (query is IPageableQuery pageableQuery)
            {
                ret.Page = pageableQuery.GetPage();
                ret.PageSize = pageableQuery.GetPageSize();
            }

            if (query is IFilterableQuery filterableQuery)
                ret.Filters = filterableQuery.GetFilters();

            if (query is IGroupableQuery groupableQuery)
                ret.Groups = groupableQuery.GetGroups();

            if (query is IAggregatableQuery aggregatableQuery)
                ret.Aggregates = aggregatableQuery.GetAggregates();

            return ret;
        }
    }
}
