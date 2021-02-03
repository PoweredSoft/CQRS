using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public interface IQueryableProvider<TSource>
    {
        Task<IQueryable<TSource>> GetQueryableAsync(object query, CancellationToken cancelllationToken = default);
    }
}
