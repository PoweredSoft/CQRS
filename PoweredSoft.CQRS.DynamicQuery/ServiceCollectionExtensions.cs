using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

        public static IServiceCollection AddDynamicQueryInterceptor<TSource, TDestination, TInterceptor>(this IServiceCollection services)
            where TInterceptor : class, IQueryInterceptor
        {
            services.TryAddTransient<TInterceptor>();
            return services.AddSingleton<IDynamicQueryInterceptorProvider<TSource, TDestination>>(
                new DynamicQueryInterceptorProvider<TSource, TDestination>(typeof(TInterceptor)));
        }

        public static IServiceCollection AddDynamicQueryInterceptors<TSource, TDestination, T1, T2>(this IServiceCollection services)
            where T1 : class, IQueryInterceptor
            where T2 : class, IQueryInterceptor
        {
            services.TryAddTransient<T1>();
            services.TryAddTransient<T2>();
            return services.AddSingleton<IDynamicQueryInterceptorProvider<TSource, TDestination>>(
                new DynamicQueryInterceptorProvider<TSource, TDestination>(typeof(T1), typeof(T2)));
        }

        public static IServiceCollection AddDynamicQueryInterceptors<TSource, TDestination, T1, T2, T3>(this IServiceCollection services)
            where T1 : class, IQueryInterceptor
            where T2 : class, IQueryInterceptor
            where T3 : class, IQueryInterceptor
        {
            services.TryAddTransient<T1>();
            services.TryAddTransient<T2>();
            services.TryAddTransient<T3>();
            return services.AddSingleton<IDynamicQueryInterceptorProvider<TSource, TDestination>>(
                new DynamicQueryInterceptorProvider<TSource, TDestination>(typeof(T1), typeof(T2), typeof(T3)));
        }

        public static IServiceCollection AddDynamicQueryInterceptors<TSource, TDestination, T1, T2, T3, T4>(this IServiceCollection services)
            where T1 : class, IQueryInterceptor
            where T2 : class, IQueryInterceptor
            where T3 : class, IQueryInterceptor
            where T4 : class, IQueryInterceptor
        {
            services.TryAddTransient<T1>();
            services.TryAddTransient<T2>();
            services.TryAddTransient<T3>();
            services.TryAddTransient<T4>();
            return services.AddSingleton<IDynamicQueryInterceptorProvider<TSource, TDestination>>(
                new DynamicQueryInterceptorProvider<TSource, TDestination>(typeof(T1), typeof(T2), typeof(T3), typeof(T4)));
        }

        public static IServiceCollection AddDynamicQueryInterceptors<TSource, TDestination, T1, T2, T3, T4, T5>(this IServiceCollection services)
            where T1 : class, IQueryInterceptor
            where T2 : class, IQueryInterceptor
            where T3 : class, IQueryInterceptor
            where T4 : class, IQueryInterceptor
            where T5 : class, IQueryInterceptor
        {
            services.TryAddTransient<T1>();
            services.TryAddTransient<T2>();
            services.TryAddTransient<T3>();
            services.TryAddTransient<T4>();
            services.TryAddTransient<T5>();
            return services.AddSingleton<IDynamicQueryInterceptorProvider<TSource, TDestination>>(
                new DynamicQueryInterceptorProvider<TSource, TDestination>(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)));
        }
    }
}
