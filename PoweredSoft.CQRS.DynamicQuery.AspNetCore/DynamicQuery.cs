using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore
{
    public class DynamicQuery<TSource, TDestination> : DynamicQuery, IDynamicQuery<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {

    }

    public class DynamicQuery<TSource, TDestination, TParams> : DynamicQuery, IDynamicQuery<TSource, TDestination, TParams>
        where TSource : class
        where TDestination : class
        where TParams : class
    {
        public TParams Params { get; set; }

        public TParams GetParams()
        {
            return Params;
        }
    }

    public class DynamicQuery : IDynamicQuery
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public List<Sort> Sorts { get; set; }
        public List<DynamicQueryAggregate> Aggregates { get; set; }
        public List<Group> Groups { get; set; }
        public List<DynamicQueryFilter> Filters { get; set; }


        public List<IAggregate> GetAggregates()
        {
            return Aggregates?.Select(t => t.ToAggregate())?.ToList();//.AsEnumerable<IAggregate>()?.ToList();
        }

        public List<IFilter> GetFilters()
        {
            return Filters?.Select(t => t.ToFilter())?.ToList();
        }

        public List<IGroup> GetGroups()
        {
            return this.Groups?.AsEnumerable<IGroup>()?.ToList();
        }

        public int? GetPage()
        {
            return this.Page;
        }

        public int? GetPageSize()
        {
            return this.PageSize;
        }

        public List<ISort> GetSorts()
        {
            return this.Sorts?.AsEnumerable<ISort>()?.ToList();
        }
    }
}
