using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using PoweredSoft.CQRS.AspNetCore.OData;
using System;
using System.Linq;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddPoweredSoftODataQueries(this IMvcBuilder builder, Action<QueryODataControllerOptions> configuration = null)
        {
            var options = new QueryODataControllerOptions();
            configuration?.Invoke(options);
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();
            builder.AddMvcOptions(o => o.Conventions.Add(new QueryODataControllerConvention(serviceProvider)));
            builder.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new QueryODataControllerFeatureProvider(serviceProvider)));
            
            if (options.FixODataSwagger)
                builder.FixODataSwagger();
            return builder;
        }

        public static IMvcBuilder FixODataSwagger(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            return builder;
        }
    }
}
