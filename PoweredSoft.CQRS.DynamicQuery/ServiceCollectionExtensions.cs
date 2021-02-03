using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.Discover;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicQuery<TSource, TDestination>(this IServiceCollection services)
            where TSource : class
            where TDestination : class
        {
            // add query handler.
            services.AddTransient<PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination>, IQueryExecutionResult<TDestination>>, DynamicQueryHandler<TSource, TDestination>>();

            // add for discovery purposes.
            var queryType = typeof(IDynamicQuery<TSource, TDestination>);
            var resultType = typeof(IQueryExecutionResult<TDestination>);
            var serviceType = typeof(DynamicQueryHandler<TSource, TDestination>);
            var queryMeta = new DynamicQueryMeta(queryType, serviceType, resultType);

            services.AddSingleton<IQueryMeta>(queryMeta);

            return services;
        }

        public static IServiceCollection AddDynamicQueryWithParams<TSource, TDestination, TParams>(this IServiceCollection services)
            where TSource : class
            where TDestination : class
        {
            // add query handler.
            services.AddTransient<PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination>, IQueryExecutionResult<TDestination>>, DynamicQueryHandler<TSource, TDestination>>();

            // add for discovery purposes.
            var queryType = typeof(IDynamicQuery<TSource, TDestination>);
            var resultType = typeof(IQueryExecutionResult<TDestination>);
            var serviceType = typeof(DynamicQueryHandler<TSource, TDestination>);
            var queryMeta = new DynamicQueryMeta(queryType, serviceType, resultType);

            // params type.
            queryMeta.ParamsType = typeof(TParams);

            services.AddSingleton<IQueryMeta>(queryMeta);

            return services;
        }
    }
}
