using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class QueryODataControllerConvention : IControllerModelConvention
    {
        private readonly IServiceProvider serviceProvider;

        public QueryODataControllerConvention(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType && controller.ControllerType.Name.Contains("QueryODataController") && controller.ControllerType.Assembly == typeof(QueryODataControllerConvention).Assembly)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var queryDiscovery = this.serviceProvider.GetRequiredService<IQueryDiscovery>();
                var query = queryDiscovery.FindQuery(genericType);
                controller.ControllerName = $"{query.LowerCamelCaseName}";
            }
        }
    }
}
