using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PoweredSoft.CQRS.GraphQL.Abstractions;

namespace PoweredSoft.CQRS.GraphQL.FluentValidation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPoweredSoftGraphQLFluentValidation(this IServiceCollection services)
        {
            services.AddTransient<IGraphQLValidationService, GraphQLFluentValidationService>();
            return services;
        }
    }
}
