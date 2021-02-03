using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public interface IDynamicQueryInterceptorProvider<TSource, TDestination>
    {
        IEnumerable<Type> GetInterceptorsTypes();
    }
}
