using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.GraphQL.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;
using System.Linq;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLDynamicQuery<TSource, TDestination> : GraphQLDynamicQuery, IDynamicQuery<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {

    }

    public class GraphQLDynamicQuery<TSource, TDestination, TParams> : GraphQLDynamicQuery<TSource, TDestination>,
        IDynamicQuery<TSource, TDestination, TParams>
        where TSource : class
        where TDestination : class
        where TParams : class
    {
        public TParams Params { get; set; }

        public TParams GetParams() => Params;
    }

    public class GraphQLDynamicQuery : IDynamicQuery
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public List<GraphQLSort> Sorts { get; set; }
        public List<GraphQLAdvanceQueryFilter> Filters { get; set; }
        public List<GraphQLAdvanceQueryGroup> Groups { get; set; }
        public List<GraphQLAdvanceQueryAggregate> Aggregates { get; set; }

        public List<IAggregate> GetAggregates()
        {
            if (Aggregates == null)
                return new List<IAggregate>();

            return Aggregates.Select(a => a.ToAggregate()).ToList();
        }

        public List<IFilter> GetFilters()
        {
            if (Filters == null)
                return new List<IFilter>();

            return Filters.Select(t => t.ToFilter()).ToList();
        }

        public List<IGroup> GetGroups()
        {
            if (Groups == null)
                return new List<IGroup>();

            return Groups.Select(t => t.ToGroup()).ToList();
        }

        public int? GetPage() => Page;

        public int? GetPageSize() => PageSize;

        public List<ISort> GetSorts()
        {
            if (Sorts == null)
                return new List<ISort>();

            return Sorts.Select(t => t.ToSort()).ToList();
        }
    }
}
