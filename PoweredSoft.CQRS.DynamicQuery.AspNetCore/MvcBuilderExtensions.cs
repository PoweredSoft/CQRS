using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.AspNetCore.Mvc;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddPoweredSoftDynamicQueries(this IMvcBuilder builder, Action<DynamicQueryControllerOptions> configuration = null)
        {
            var options = new DynamicQueryControllerOptions();
            configuration?.Invoke(options);
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();
            builder.AddMvcOptions(o => o.Conventions.Add(new DynamicQueryControllerConvention(serviceProvider)));
            builder.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new DynamicQueryControllerFeatureProvider(serviceProvider)));
            return builder;
        }
    }
}
