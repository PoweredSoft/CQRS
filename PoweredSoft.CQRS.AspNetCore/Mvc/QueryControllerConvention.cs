using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class QueryControllerConvention : IControllerModelConvention
    {
        private readonly IServiceProvider serviceProvider;

        public QueryControllerConvention(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType && controller.ControllerType.Name.Contains("QueryController") && controller.ControllerType.Assembly == typeof(QueryControllerConvention).Assembly)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var queryDiscovery = this.serviceProvider.GetRequiredService<IQueryDiscovery>();
                var query = queryDiscovery.FindQuery(genericType);
                controller.ControllerName = query.LowerCamelCaseName;
            }
        }
    }
}
