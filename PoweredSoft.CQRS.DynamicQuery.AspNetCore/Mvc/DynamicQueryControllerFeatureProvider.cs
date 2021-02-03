using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes;
using PoweredSoft.CQRS.DynamicQuery.Discover;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore.Mvc
{
    public class DynamicQueryControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ServiceProvider serviceProvider;

        public DynamicQueryControllerFeatureProvider(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var queryDiscovery = this.serviceProvider.GetRequiredService<IQueryDiscovery>();
            foreach (var f in queryDiscovery.GetQueries())
            {
                var ignoreAttribute = f.QueryType.GetCustomAttribute<QueryControllerIgnoreAttribute>();
                if (ignoreAttribute != null)
                    continue;

                if (f.Category != "DynamicQuery")
                    continue;

                if (f is DynamicQueryMeta dynamicQueryMeta)
                {
                    if (dynamicQueryMeta.ParamsType == null)
                    {
                        var controllerType = typeof(DynamicQueryController<,,>).MakeGenericType(f.QueryType, dynamicQueryMeta.SourceType, dynamicQueryMeta.DestinationType);
                        var controllerTypeInfo = controllerType.GetTypeInfo();
                        feature.Controllers.Add(controllerTypeInfo);
                    }
                    else
                    {
                        var controllerType = typeof(DynamicQueryController<,,,>).MakeGenericType(f.QueryType, dynamicQueryMeta.SourceType, dynamicQueryMeta.DestinationType, dynamicQueryMeta.ParamsType);
                        var controllerTypeInfo = controllerType.GetTypeInfo();
                        feature.Controllers.Add(controllerTypeInfo);
                    }
                }
            }
        }
    }
}
