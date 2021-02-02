using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.Abstractions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuery<TQuery, TQueryResult, TQueryHandler>(this IServiceCollection services)
            where TQuery : class
            where TQueryHandler : class, IQueryHandler<TQuery, TQueryResult>
        {
            // add handler to DI.
            services.AddTransient<IQueryHandler<TQuery, TQueryResult>, TQueryHandler>();

            // add for discovery purposes.
            var queryMeta = new QueryMeta(typeof(TQuery), typeof(IQueryHandler<TQuery, TQueryResult>), typeof(TQueryResult));
            services.AddSingleton<IQueryMeta>(queryMeta);

            return services;
        }

        public static IServiceCollection AddCommand<TCommand, TCommandResult, TCommandHandler>(this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand, TCommandResult>
        {
            // add handler to DI.
            services.AddTransient<ICommandHandler<TCommand, TCommandResult>, TCommandHandler>();

            // add for discovery purposes.
            var commandMeta = new CommandMeta(typeof(TCommand), typeof(ICommandHandler<TCommand>), typeof(TCommandResult));
            services.AddSingleton<ICommandMeta>(commandMeta);

            return services;
        }

        public static IServiceCollection AddCommand<TCommand, TCommandHandler>(this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            // add handler to DI.
            services.AddTransient<ICommandHandler<TCommand>, TCommandHandler>();

            // add for discovery purposes.
            var commandMeta = new CommandMeta(typeof(TCommand), typeof(ICommandHandler<TCommand>));
            services.AddSingleton<ICommandMeta>(commandMeta);

            return services;
        }
    }
}
