using System;
using System.Collections.Generic;

namespace PoweredSoft.CQRS.DynamicQuery.Abstractions
{
    public class DynamicQueryInterceptorProvider<TSource, TDestination> : IDynamicQueryInterceptorProvider<TSource, TDestination>
    {
        private readonly Type[] types;

        public DynamicQueryInterceptorProvider(params Type[] types)
        {
            this.types = types;
        }

        public IEnumerable<Type> GetInterceptorsTypes()
        {
            return types;
        }
    }
}
