using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes;
using PoweredSoft.CQRS.AspNetCore.OData.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.OData
{
    public class QueryODataControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ServiceProvider serviceProvider;

        public QueryODataControllerFeatureProvider(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var queryDiscovery = this.serviceProvider.GetRequiredService<IQueryDiscovery>();
            foreach (var f in queryDiscovery.GetQueries())
            {
                var ignoreAttribute = f.QueryType.GetCustomAttribute<QueryOdataControllerIgnoreAttribute>();
                if (ignoreAttribute != null)
                    continue;

                if (f.Category != "BasicQuery")
                    continue;

                var isQueryable = f.QueryResultType.Namespace == "System.Linq" && f.QueryResultType.Name.Contains("IQueryable");
                if (!isQueryable)
                    continue;

                var controllerType = typeof(QueryODataController<,>).MakeGenericType(f.QueryType, f.QueryResultType);
                var controllerTypeInfo = controllerType.GetTypeInfo();
                feature.Controllers.Add(controllerTypeInfo);
            }
        }
    }
}
