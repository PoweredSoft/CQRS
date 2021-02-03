using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public interface IAlterQueryableService<TSource, TDestination>
    {
        Task<IQueryable<TSource>> AlterQueryableAsync(IQueryable<TSource> query, IDynamicQuery dynamicQuery);
    }
    public interface IAlterQueryableService<TSource, TDestination, TParams>
        where TParams : class
    {
        Task<IQueryable<TSource>> AlterQueryableAsync(IQueryable<TSource> query, IDynamicQueryParams<TParams> dynamicQuery);
    }
}
