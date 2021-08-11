using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.FluentValidation
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddFluentValidator<T, TValidator>(this IServiceCollection services)
            where TValidator : class, IValidator<T>
        {
            services.AddTransient<IValidator<T>, TValidator>();
            return services;
        }

        public static IServiceCollection AddCommandWithValidator<TCommand, TCommandHandler, TValidator>(this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand>
            where TValidator : class, IValidator<TCommand>
        {
            return services.AddCommand<TCommand, TCommandHandler>()
                .AddFluentValidator<TCommand, TValidator>();
        }

        public static IServiceCollection AddCommandWithValidator<TCommand, TCommandResult, TCommandHandler, TValidator>(this IServiceCollection services)
            where TCommand : class
            where TCommandHandler : class, ICommandHandler<TCommand, TCommandResult>
            where TValidator : class, IValidator<TCommand>
        {
            return services.AddCommand<TCommand, TCommandResult, TCommandHandler>()
               .AddFluentValidator<TCommand, TValidator>();
        }

        public static IServiceCollection AddQueryWithValidator<TQuery, TQueryResult, TQueryHandler, TValidator>(this IServiceCollection services)
            where TQuery : class
            where TQueryHandler : class, IQueryHandler<TQuery, TQueryResult>
            where TValidator : class, IValidator<TQuery>
        {
            services.AddQuery<TQuery, TQueryResult, TQueryHandler>()
                    .AddFluentValidator<TQuery, TValidator>();
            return services;
        }
    }
}
