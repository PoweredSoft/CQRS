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
        public static IServiceCollection AddDynamicQuery<TSourceAndDestination>(this IServiceCollection services, string name = null)
            where TSourceAndDestination : class
            => AddDynamicQuery<TSourceAndDestination, TSourceAndDestination>(services, name: name);

        public static IServiceCollection AddDynamicQuery<TSource, TDestination>(this IServiceCollection services, string name = null)
            where TSource : class
            where TDestination : class
        {
            // add query handler.
            services.AddTransient<PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination>, IQueryExecutionResult<TDestination>>, DynamicQueryHandler<TSource, TDestination>>();

            // add for discovery purposes.
            var queryType = typeof(IDynamicQuery<TSource, TDestination>);
            var resultType = typeof(IQueryExecutionResult<TDestination>);
            var serviceType = typeof(DynamicQueryHandler<TSource, TDestination>);
            var queryMeta = new DynamicQueryMeta(queryType, serviceType, resultType)
            {
                OverridableName = name
            };

            services.AddSingleton<IQueryMeta>(queryMeta);

            return services;
        }

        public static IServiceCollection AddDynamicQueryWithParams<TSourceAndDestination, TParams>(this IServiceCollection services, string name = null)
            where TSourceAndDestination : class
            where TParams : class
            => AddDynamicQueryWithParams<TSourceAndDestination, TSourceAndDestination, TParams>(services, name: name);

            public static IServiceCollection AddDynamicQueryWithParams<TSource, TDestination, TParams>(this IServiceCollection services, string name = null)
            where TSource : class
            where TDestination : class
            where TParams : class
        {
            // add query handler.
            services.AddTransient<PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination, TParams>, IQueryExecutionResult<TDestination>>, DynamicQueryHandler<TSource, TDestination, TParams>>();

            // add for discovery purposes.
            var queryType = typeof(IDynamicQuery<TSource, TDestination, TParams>);
            var resultType = typeof(IQueryExecutionResult<TDestination>);
            var serviceType = typeof(DynamicQueryHandler<TSource, TDestination>);
            var queryMeta = new DynamicQueryMeta(queryType, serviceType, resultType)
            {

                // params type.
                ParamsType = typeof(TParams),
                OverridableName = name
            };

            services.AddSingleton<IQueryMeta>(queryMeta);

            return services;
        }

        public static IServiceCollection AddAlterQueryable<TSourceAndDestination, TService>(this IServiceCollection services)
            where TService : class, IAlterQueryableService<TSourceAndDestination, TSourceAndDestination>
        {
            return services.AddTransient<IAlterQueryableService<TSourceAndDestination, TSourceAndDestination>, TService>();
        }

        public static IServiceCollection AddAlterQueryable<TSource, TDestination, TService>(this IServiceCollection services)
            where TService : class, IAlterQueryableService<TSource, TDestination>
        {
            return services.AddTransient<IAlterQueryableService<TSource, TDestination>, TService>();
        }

        public static IServiceCollection AddAlterQueryableWithParams<TSourceAndTDestination, TParams, TService>
            (this IServiceCollection services)
            where TParams : class
            where TService : class, IAlterQueryableService<TSourceAndTDestination, TSourceAndTDestination, TParams>
        {
            return services.AddTransient<IAlterQueryableService< TSourceAndTDestination, TSourceAndTDestination, TParams>, TService>();
        }

        public static IServiceCollection AddAlterQueryableWithParams<TSource, TDestination, TParams, TService>
            (this IServiceCollection services)
            where TParams : class
            where TService : class, IAlterQueryableService<TSource, TDestination, TParams>
        {
            return services.AddTransient<IAlterQueryableService<TSource, TDestination, TParams>, TService>();
        }

        public static IServiceCollection AddQueryConvertInterceptor<TSource, TDestination, TService>(this IServiceCollection services)
            where TService : class, IQueryConvertInterceptor<TSource, TDestination>
        {
            return services.AddTransient<IQueryConvertInterceptor<TSource, TDestination>, TService>();
        }

        public static IServiceCollection AddAfterReadInterceptorAsync<TSource, TService>(this IServiceCollection services)
            where TService : class, IAfterReadInterceptorAsync<TSource>
        {
            return services.AddTransient<IAfterReadInterceptorAsync<TSource>, TService>();
        }

        public static IServiceCollection AddAfterReadInterceptorAsync<TSource, TDestination, TService>(this IServiceCollection services)
            where TService : class, IAfterReadInterceptorAsync<TSource, TDestination>
        {
            return services.AddTransient<IAfterReadInterceptorAsync<TSource, TDestination>, TService>();
        }
    }
}
