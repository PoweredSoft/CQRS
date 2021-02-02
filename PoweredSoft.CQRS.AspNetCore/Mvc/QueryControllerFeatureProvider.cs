using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class QueryControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ServiceProvider serviceProvider;

        public QueryControllerFeatureProvider(ServiceProvider serviceProvider)
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

                var controllerType = typeof(QueryController<,>).MakeGenericType(f.QueryType, f.QueryResultType);
                var controllerTypeInfo = controllerType.GetTypeInfo();
                feature.Controllers.Add(controllerTypeInfo);
            }
        }
    }
}
