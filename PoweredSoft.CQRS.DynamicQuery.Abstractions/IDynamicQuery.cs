using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public interface IDynamicQuery<TSource, TDestination> : IDynamicQuery
        where TSource : class
        where TDestination : class
    {

    }

    public interface IDynamicQuery
        
    {
        List<IFilter> GetFilters();
        List<IGroup> GetGroups();
        List<ISort> GetSorts();
        List<IAggregate> GetAggregates();
        int? GetPage();
        int? GetPageSize();
    }
}
