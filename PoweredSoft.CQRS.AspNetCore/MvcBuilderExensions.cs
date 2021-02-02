using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddPoweredSoftQueries(this IMvcBuilder builder, Action<QueryControllerOptions> configuration = null)
        {
            var options = new QueryControllerOptions();
            configuration?.Invoke(options);
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();
            builder.AddMvcOptions(o => o.Conventions.Add(new QueryControllerConvention(serviceProvider)));
            builder.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new QueryControllerFeatureProvider(serviceProvider)));
            return builder;
        }

        public static IMvcBuilder AddPoweredSoftCommands(this IMvcBuilder builder)
        {
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();
            builder.AddMvcOptions(o => o.Conventions.Add(new CommandControllerConvention(serviceProvider)));
            builder.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new CommandControllerFeatureProvider(serviceProvider)));
            return builder;
        }
    }
}
