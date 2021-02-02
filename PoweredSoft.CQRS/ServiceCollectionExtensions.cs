using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPoweredSoftCQRS(this IServiceCollection services)
        {
            services.TryAddTransient<IQueryDiscovery, QueryDiscovery>();
            services.TryAddTransient<ICommandDiscovery, CommandDiscovery>();
            return services;
        }
    }
}
